using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using XMS.Application.Common;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Domain.OneS;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            costGroup.MapPost("/notify/write-off-non-cash", NotifyWriteOffNonCash)
                .WithName(nameof(NotifyWriteOffNonCash))
                .WithSummary(nameof(NotifyWriteOffNonCash))
                .WithDescription("Notify Document_СписаниеБезналичныхДенежныхСредств");

            costGroup.MapGet("/write-off-non-cash/{refKey}", GetWriteOffNonCash)
                .WithName(nameof(GetWriteOffNonCash))
                .WithSummary(nameof(GetWriteOffNonCash))
                .WithDescription("Get Document_СписаниеБезналичныхДенежныхСредств");

            builder.MapGet("/integration/cost/write-off-non-cash", LoadWriteOffNonCashByDate)
                .WithTags("XMS Cost")
                .WithName(nameof(LoadWriteOffNonCashByDate))
                .WithSummary(nameof(LoadWriteOffNonCashByDate))
                .WithDescription("Load Document_СписаниеБезналичныхДенежныхСредств by Date from OneS Ut");

            builder.MapPut("/integration/cost/write-off-non-cash/reload", ReLoadWriteOffNonCashByDate)
                .WithTags("XMS Cost")
                .WithName(nameof(ReLoadWriteOffNonCashByDate))
                .WithSummary(nameof(ReLoadWriteOffNonCashByDate))
                .WithDescription("Load Document_СписаниеБезналичныхДенежныхСредств by Date from OneS Ut"); ;

            return builder;
        }

        /// <summary>
        /// Notify Document_СписаниеБезналичныхДенежныхСредств
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="notifyBody"></param>
        /// <returns></returns>
        private static Results<Ok, BadRequest<string>> NotifyWriteOffNonCash(
            //[FromServices] IOneSNotifyService notifyService,
            [FromServices] ILoggerFactory loggerFactory,
            [FromBody] OneSNotifyBody notifyBody)
        {
            var logger = loggerFactory.CreateLogger(nameof(CostEndpoints));

            if (!Guid.TryParse(notifyBody.Ref_Key, out Guid refKey))
            {
                logger.LogError("{Source} Unable to parse Ref_Key {NotifyBody}", nameof(NotifyWriteOffNonCash), notifyBody);
                return TypedResults.BadRequest($"Unable to parse Ref_Key {notifyBody.Ref_Key}");
            }

            logger.LogDebug("{Source} {notifyBody}", nameof(NotifyWriteOffNonCash), notifyBody);

            return TypedResults.Ok();
        }

        /// <summary>
        /// Get Document_СписаниеБезналичныхДенежныхСредств
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="refKey"></param>
        /// <returns></returns>
        private static Results<Ok, BadRequest<string>> GetWriteOffNonCash(
           //[FromServices] IOneSNotifyService notifyService,
           [FromServices] ILoggerFactory loggerFactory,
           [FromQuery] string refKey)
        {
            var logger = loggerFactory.CreateLogger(nameof(CostEndpoints));

            logger.LogDebug("{Source} {kefKey}", nameof(GetWriteOffNonCash), refKey);

            return TypedResults.Ok();
        }

        /// <summary>
        /// Load Document_СписаниеБезналичныхДенежныхСредств from OneS Ut
        /// </summary>
        /// <param name="documentService"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private static async Task<Results<Ok<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>>, BadRequest<string>>> LoadWriteOffNonCashByDate(
            [FromServices] IDocument_СписаниеБезналичныхДенежныхСредств_Service documentService,
            [FromQuery] DateTime date)
        {
            var result = await documentService.LoadListAsyncByDate(date);

            return TypedResults.Ok(result);
        }

        private static async Task<Results<Ok<int>, BadRequest<string>>> ReLoadWriteOffNonCashByDate(
            [FromServices] IDocument_СписаниеБезналичныхДенежныхСредств_Service documentService,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to)
        {
            var result = await documentService.ReloadListAsync(from, to);

            return TypedResults.Ok(result);
        }
    }
}
