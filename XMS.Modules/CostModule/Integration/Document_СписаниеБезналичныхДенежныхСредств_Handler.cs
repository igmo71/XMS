using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Data;
using XMS.Application.Abstractions.EventBus;
using XMS.Application.Abstractions.Integration;
using XMS.Application.Common;
using XMS.Application.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature;
using XMS.Application.Integration.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;
using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Integration;

internal class Document_СписаниеБезналичныхДенежныхСредств_Handler(
    IDbContextFactoryProxy dbFactory,
    IOneCUtService oneCUtService,
    ILogger<Document_СписаниеБезналичныхДенежныхСредств_Handler> logger)
    : BaseService, IAppEventHandler<Document_СписаниеБезналичныхДенежныхСредств>
{
    public async Task HandleAsync(Document_СписаниеБезналичныхДенежныхСредств documentEvent, CancellationToken ct = default)
    {
        using var activity = StartActivity();

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Start {@documentEvent}", nameof(HandleAsync), documentEvent);

        using var dbContext = dbFactory.CreateDbContext();

        var paymentDetails = GetPaymentDetails(documentEvent);

        foreach (var paymentDetail in paymentDetails)
        {
            if (paymentDetail.Value.СтатьяДвиженияДенежныхСредств_Key is null)
            {
                if (logger.IsEnabled(LogLevel.Error))
                    logger.LogError("{Source} - PaymentDetail СтатьяДвиженияДенежныхСредств_Key is null {@documentEvent}", nameof(HandleAsync), documentEvent);
                continue;
            }

            var catalog_СтатьяДДС = await oneCUtService.GetCatalog_СтатьиДвиженияДенежныхСредств_Async((Guid)paymentDetail.Value.СтатьяДвиженияДенежныхСредств_Key, ct);

            if (catalog_СтатьяДДС is null)
            {
                if (logger.IsEnabled(LogLevel.Error))
                    logger.LogError("{Source} - Catalog_СтатьяДДС nor found {@documentEvent}", nameof(HandleAsync), documentEvent);
                continue;
            }

            await CreateOrUpdateCostItem(dbContext, catalog_СтатьяДДС, ct);

            if (documentEvent.КСЗ_КатегорияЗатрат_Key is null || documentEvent.КСЗ_КатегорияЗатрат_Key == Guid.Empty)
            {
                if (logger.IsEnabled(LogLevel.Error))
                    logger.LogError("{Source} - КатегорияЗатрат_Key is null or Empty {@documentEvent}", nameof(HandleAsync), documentEvent);
                continue;
            }

            await CreateCostCategoryItemIfNotExists(dbContext, (Guid)documentEvent.КСЗ_КатегорияЗатрат_Key, catalog_СтатьяДДС.Ref_Key, ct);

            await CreateOrUpdateCostAllocation(dbContext, documentEvent, paymentDetail, ct);
        }

        await dbContext.SaveChangesAsync(ct);

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Ok {@documentEvent}", nameof(HandleAsync), documentEvent);
    }

    private Dictionary<int, Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа> GetPaymentDetails(Document_СписаниеБезналичныхДенежныхСредств documentEvent)
    {
        Dictionary<int, Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа> paymentDetails = [];

        if (documentEvent.СтатьяДвиженияДенежныхСредств_Key != null && documentEvent.СтатьяДвиженияДенежныхСредств_Key != Guid.Empty)
        {
            paymentDetails.Add(0, new Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа
            {
                LineNumber = 0,
                СтатьяДвиженияДенежныхСредств_Key = documentEvent.СтатьяДвиженияДенежныхСредств_Key,
                Сумма = documentEvent.СуммаДокумента
            });
        }
        else
        {
            paymentDetails = documentEvent.РасшифровкаПлатежа?
                .ToDictionary(e => e.LineNumber) ?? [];
        }

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - RefKeys: {@paymentDetails} {@documentEvent}", nameof(HandleAsync), paymentDetails, documentEvent);
        return paymentDetails;
    }

    private async Task CreateOrUpdateCostItem(
        IApplicationDbContext dbContext,
        Catalog_СтатьиДвиженияДенежныхСредств catalog_СтатьяДДС,
        CancellationToken ct)
    {
        var existingCostItem = await dbContext.Set<CostItem>()
                        .FirstOrDefaultAsync(e => e.Id == catalog_СтатьяДДС.Ref_Key, cancellationToken: ct);

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - existingCostItem: {@existingCostItem}", nameof(HandleAsync), existingCostItem);

        if (existingCostItem == null)
        {
            var createdCostItem = dbContext.Set<CostItem>().Add(new CostItem
            {
                Id = catalog_СтатьяДДС.Ref_Key,
                Name = catalog_СтатьяДДС.Description ?? string.Empty
            }).Entity;

            if (logger.IsEnabled(LogLevel.Debug))
                logger.LogDebug("{Source} - createdCostItem: {@createdCostItem}", nameof(HandleAsync), createdCostItem);
        }
        else
        {
            existingCostItem.Name = catalog_СтатьяДДС.Description ?? string.Empty;
        }
    }

    private async Task CreateCostCategoryItemIfNotExists(
        IApplicationDbContext dbContext,
        Guid КатегорияЗатрат_Key,
        Guid СтатьяДДС_Key,
        CancellationToken ct)
    {
        var existingCostCategoryItem = await dbContext.Set<CostCategoryItem>()
                        .FirstOrDefaultAsync(e => e.CategoryId == КатегорияЗатрат_Key && e.ItemId == СтатьяДДС_Key, ct);

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - existingCostCategoryItem: {@existingCostCategoryItem}", nameof(HandleAsync), existingCostCategoryItem);

        if (existingCostCategoryItem == null)
        {
            var createdCostCategoryItem = dbContext.Set<CostCategoryItem>().Add(new CostCategoryItem
            {
                CategoryId = КатегорияЗатрат_Key,
                ItemId = СтатьяДДС_Key
            }).Entity;

            if (logger.IsEnabled(LogLevel.Debug))
                logger.LogDebug("{Source} - createdCostCategoryItem: {@createdCostCategoryItem}", nameof(HandleAsync), createdCostCategoryItem);
        }
    }

    private async Task CreateOrUpdateCostAllocation(
        IApplicationDbContext dbContext,
        Document_СписаниеБезналичныхДенежныхСредств documentEvent,
        KeyValuePair<int, Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа> paymentDetail,
        CancellationToken ct)
    {
        var existingCostAllocations = await dbContext.Set<CostAllocation>()
                        .Where(e => e.PaymentVoucherId == documentEvent.Ref_Key && e.PaymentDetailLineNumber == paymentDetail.Value.LineNumber)
                        .ToListAsync(ct);

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - existingCostAllocations: {@existingCostAllocation} {@documentEvent} {@paymentDetail}",
                nameof(HandleAsync), existingCostAllocations, documentEvent, paymentDetail);

        if (existingCostAllocations.Count == 0)
        {
            var createdCostAllocation = dbContext.Set<CostAllocation>().Add(new CostAllocation
            {
                IsDeleted = documentEvent.DeletionMark || !documentEvent.Posted,
                DeletedAt = documentEvent.DeletionMark || !documentEvent.Posted ? DateTime.UtcNow : null,
                PaymentVoucherId = documentEvent.Ref_Key,
                PaymentVoucherType = PaymentVoucherType.Bank,
                Number = documentEvent.Number,
                Date = documentEvent.Date,
                CostCategoryId = documentEvent.КСЗ_КатегорияЗатрат_Key == Guid.Empty ? null : documentEvent.КСЗ_КатегорияЗатрат_Key,
                BusinessOperation = documentEvent.ХозяйственнаяОперация,
                AuthorId = documentEvent.Автор_Key,
                Comment = documentEvent.Комментарий,
                PaymentPurpose = documentEvent.НазначениеПлатежа,
                CostItemId = paymentDetail.Value.СтатьяДвиженияДенежныхСредств_Key,
                TotalAmount = paymentDetail.Value.Сумма,
                PaymentDetailLineNumber = paymentDetail.Value.LineNumber
            }).Entity;

            if (logger.IsEnabled(LogLevel.Debug))
                logger.LogDebug("{Source} - createdCostAllocation: {@createdCostAllocation} {@documentEvent} {@paymentDetail}",
                    nameof(HandleAsync), createdCostAllocation, documentEvent, paymentDetail);
        }
        else if (existingCostAllocations.Count == 1)
        {
            existingCostAllocations[0].IsDeleted = documentEvent.DeletionMark || !documentEvent.Posted;
            existingCostAllocations[0].DeletedAt = documentEvent.DeletionMark || !documentEvent.Posted ? DateTime.UtcNow : null;
            existingCostAllocations[0].PaymentVoucherType = PaymentVoucherType.Bank;
            existingCostAllocations[0].Number = documentEvent.Number;
            existingCostAllocations[0].Date = documentEvent.Date;
            existingCostAllocations[0].CostCategoryId = documentEvent.КСЗ_КатегорияЗатрат_Key == Guid.Empty ? null : documentEvent.КСЗ_КатегорияЗатрат_Key;
            existingCostAllocations[0].BusinessOperation = documentEvent.ХозяйственнаяОперация;
            existingCostAllocations[0].AuthorId = documentEvent.Автор_Key;
            existingCostAllocations[0].Comment = documentEvent.Комментарий;
            existingCostAllocations[0].PaymentPurpose = documentEvent.НазначениеПлатежа;
            existingCostAllocations[0].CostItemId = paymentDetail.Value.СтатьяДвиженияДенежныхСредств_Key;
            existingCostAllocations[0].TotalAmount = paymentDetail.Value.Сумма;
            existingCostAllocations[0].PaymentDetailLineNumber = paymentDetail.Value.LineNumber;
        }
        else
        {
            if (logger.IsEnabled(LogLevel.Error))
                logger.LogError("{Source} - createdCostAllocations too much for one paymentDetail {@documentEvent}", nameof(HandleAsync), documentEvent);
        }
    }
}
