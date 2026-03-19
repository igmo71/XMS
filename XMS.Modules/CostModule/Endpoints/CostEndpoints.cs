using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using XMS.Application;
using XMS.Application.Common;
using XMS.Application.Common.Integration;
using XMS.Integration.OneC;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Domain.OneS;
using XMS.Modules.CostModule.EventBus;

namespace XMS.Modules.CostModule.Endpoints
{
    public static class CostEndpoints
    {
        public static IEndpointRouteBuilder MapCostEndpoints(this IEndpointRouteBuilder builder)
        {
            var costGroup = builder.MapGroup("/api/cost")
                .WithTags("XMS Cost")
                //.AddEndpointFilter<ApiKeyAuthFilter>()
                //.ProducesProblem(401)
                .ProducesValidationProblem();

            // Document_СписаниеБезналичныхДенежныхСредств: write-off-non-cash

            costGroup.MapGet("/write-off-non-cash", GetWriteOffNonCash)
                .WithName(nameof(GetWriteOffNonCash))
                .WithSummary(nameof(GetWriteOffNonCash))
                .WithDescription("Get Document_СписаниеБезналичныхДенежныхСредств by Ref_Key");

            costGroup.MapGet("/write-off-non-cash/{refKey}", GetWriteOffNonCashByRefKey)
                .WithName(nameof(GetWriteOffNonCashByRefKey))
                .WithSummary(nameof(GetWriteOffNonCashByRefKey))
                .WithDescription("Get Document_СписаниеБезналичныхДенежныхСредств by Ref_Key");

            builder.MapGet("/integration/cost/write-off-non-cash", LoadWriteOffNonCashByDate)
                .WithTags("XMS Cost")
                .WithName(nameof(LoadWriteOffNonCashByDate))
                .WithSummary(nameof(LoadWriteOffNonCashByDate))
                .WithDescription("Load Document_СписаниеБезналичныхДенежныхСредств by Date from OneS Ut");

            builder.MapPut("/integration/cost/write-off-non-cash/reload", ReLoadWriteOffNonCashByDate)
                .WithTags("XMS Cost")
                .WithName(nameof(ReLoadWriteOffNonCashByDate))
                .WithSummary(nameof(ReLoadWriteOffNonCashByDate))
                .WithDescription("Download Document_СписаниеБезналичныхДенежныхСредств from OneS Ut for a cpecific date period and save them to the DB");

            costGroup.MapPost("/notify/write-off-non-cash", NotifyWriteOffNonCashChangedAsync)
                .WithName(nameof(NotifyWriteOffNonCashChangedAsync))
                .WithSummary(nameof(NotifyWriteOffNonCashChangedAsync))
                .WithDescription("Notify Document_СписаниеБезналичныхДенежныхСредств Changed");

            return builder;
        }



        /// <summary>
        /// Get Document_СписаниеБезналичныхДенежныхСредств list from DB by DocumentQueryParameters
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="refKey"></param>
        /// <returns></returns>
        private static async Task<Results<Ok<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>>, BadRequest<string>>> GetWriteOffNonCash(
            [FromServices] IDocument_СписаниеБезналичныхДенежныхСредств_Service documentService,
            [FromServices] ILoggerFactory loggerFactory,
            [FromQuery] string? numberTerm = null,
            [FromQuery] DateTime? from = null,
            [FromQuery] DateTime? to = null,
            [FromQuery] int? skip = AppSettings.Default.Skip,
            [FromQuery] int? take = AppSettings.Default.Take,
            CancellationToken ct = default)
        {
            var logger = loggerFactory.CreateLogger(nameof(CostEndpoints));

            var parameters = new DocumentQueryParameters(numberTerm, from, to, skip, take);

            var result = await documentService.GetListAsync(parameters, ct);

            logger.LogDebug("{Source} {DocumentQueryParameters} {Documents}", nameof(GetWriteOffNonCash), parameters, result);

            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Get Document_СписаниеБезналичныхДенежныхСредств from DB by Ref_Key
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="refKey"></param>
        /// <returns></returns>
        private static async Task<Results<Ok<Document_СписаниеБезналичныхДенежныхСредств>, NotFound<string>>> GetWriteOffNonCashByRefKey(
            [FromServices] IDocument_СписаниеБезналичныхДенежныхСредств_Service documentService,
            [FromServices] ILoggerFactory loggerFactory,
            [FromRoute] string refKey,
            CancellationToken ct = default)
        {
            var logger = loggerFactory.CreateLogger(nameof(CostEndpoints));

            var result = await documentService.GetAsync(refKey, ct);

            logger.LogDebug("{Source} {DocumentQueryParameters} {Document}", nameof(GetWriteOffNonCashByRefKey), refKey, result);

            return result is null ? TypedResults.NotFound($"Document Not Found by Ref_Key {refKey}") : TypedResults.Ok(result);
        }

        /// <summary>
        /// Load Document_СписаниеБезналичныхДенежныхСредств from OneS Ut
        /// Загрузить документы СписаниеБезналичныхДенежныхСредств за определенную дату из 1С УТ.
        /// </summary>
        /// <param name="documentService"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private static async Task<Results<Ok<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>>, BadRequest<string>>> LoadWriteOffNonCashByDate(
            [FromServices] IDocument_СписаниеБезналичныхДенежныхСредств_Service documentService,
            [FromQuery] DateTime date)
        {
            var result = await documentService.LoadListAsyncByDate(date);

            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Download Document_СписаниеБезналичныхДенежныхСредств from OneS Ut for a cpecific date period and save them to the DB
        /// Загрузить документы СписаниеБезналичныхДенежныхСредств за определенный период из 1С УТ и записать их БД
        /// </summary>
        /// <param name="documentService"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private static async Task<Results<Ok<int>, BadRequest<string>>> ReLoadWriteOffNonCashByDate(
            [FromServices] IDocument_СписаниеБезналичныхДенежныхСредств_Service documentService,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to)
        {
            var result = await documentService.ReloadListAsync(from, to);

            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Notify Document_СписаниеБезналичныхДенежныхСредств from OneS
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="notifyBody"></param>
        /// <returns></returns>
        private static async Task<IResult> NotifyWriteOffNonCashChangedAsync(
            [FromServices] IPublishEndpoint publishEndpoint,
            [FromServices] ILoggerFactory loggerFactory,
            [FromBody] Document_СписаниеБезналичныхДенежныхСредств_Changed notifyBody)
        {
            notifyBody.EventOperation = EventOperation.CreateOrUpdate;
            await publishEndpoint.Publish(notifyBody);

            return TypedResults.Ok();
        }
    }
}
