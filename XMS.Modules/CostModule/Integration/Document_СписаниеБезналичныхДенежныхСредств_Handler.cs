using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XMS.Application.Abstractions.Data;
using XMS.Application.Abstractions.EventBus;
using XMS.Application.Abstractions.Integration;
using XMS.Application.Common;
using XMS.Integrations.OneC.Ut.Features.Document_СписаниеБезналичныхДенежныхСредств_Feature;
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
        using var activity = this.StartActivity();

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Start {@documentEvent}", nameof(HandleAsync), documentEvent);

        using var dbContext = dbFactory.CreateDbContext();

        List<Guid> catalog_СтатьяДДС_RefKeys = [];

        if (documentEvent.СтатьяДвиженияДенежныхСредств_Key != null && documentEvent.СтатьяДвиженияДенежныхСредств_Key != Guid.Empty)
            catalog_СтатьяДДС_RefKeys.Add((Guid)documentEvent.СтатьяДвиженияДенежныхСредств_Key);
        else
        {
            catalog_СтатьяДДС_RefKeys = documentEvent.РасшифровкаПлатежа?
                .Where(e => e.СтатьяДвиженияДенежныхСредств_Key != null && e.СтатьяДвиженияДенежныхСредств_Key != Guid.Empty)
                .Select(e => (Guid)e.СтатьяДвиженияДенежныхСредств_Key!)
                .ToList() ?? [];
        }

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - RefKeys: {@catalog_СтатьяДДС_RefKeys} {@documentEvent}", nameof(HandleAsync), catalog_СтатьяДДС_RefKeys, documentEvent);

        foreach (var key in catalog_СтатьяДДС_RefKeys)
        {
            var catalog_СтатьяДДС = await oneCUtService.GetCatalog_СтатьиДвиженияДенежныхСредств_Async(key, ct);
            if (catalog_СтатьяДДС != null)
            {
                var existingCostItem = await dbContext.Set<CostItem>()
                    .FirstOrDefaultAsync(e => e.Id == catalog_СтатьяДДС.Ref_Key, cancellationToken: ct);

                if (logger.IsEnabled(LogLevel.Debug))
                    logger.LogDebug("{Source} - existingCostItem: {@existingCostItem} {@documentEvent}", nameof(HandleAsync), existingCostItem, documentEvent);

                if (existingCostItem == null)
                {
                    var createdCostItem = dbContext.Set<CostItem>().Add(new CostItem
                    {
                        Id = catalog_СтатьяДДС.Ref_Key,
                        Name = catalog_СтатьяДДС.Description ?? string.Empty
                    }).Entity;

                    if (logger.IsEnabled(LogLevel.Debug))
                        logger.LogDebug("{Source} - createdCostItem: {@createdCostItem} {@documentEvent}", nameof(HandleAsync), createdCostItem, documentEvent);
                }
                else
                {
                    existingCostItem.Name = catalog_СтатьяДДС.Description ?? string.Empty;
                }

                if (documentEvent.КСЗ_КатегорияЗатрат_Key != null && documentEvent.КСЗ_КатегорияЗатрат_Key != Guid.Empty)
                {
                    var existingCostCategoryItem = await dbContext.Set<CostCategoryItem>()
                        .FirstOrDefaultAsync(e => e.CategoryId == documentEvent.КСЗ_КатегорияЗатрат_Key && e.ItemId == catalog_СтатьяДДС.Ref_Key, ct);

                    if (logger.IsEnabled(LogLevel.Debug))
                        logger.LogDebug("{Source} - existingCostCategoryItem: {@existingCostCategoryItem} {@documentEvent}", nameof(HandleAsync), existingCostCategoryItem, documentEvent);

                    if (existingCostCategoryItem == null)
                    {
                        var createdCostCategoryItem = dbContext.Set<CostCategoryItem>().Add(new CostCategoryItem
                        {
                            CategoryId = (Guid)documentEvent.КСЗ_КатегорияЗатрат_Key,
                            ItemId = catalog_СтатьяДДС.Ref_Key
                        }).Entity;

                        if (logger.IsEnabled(LogLevel.Debug))
                            logger.LogDebug("{Source} - createdCostCategoryItem: {@createdCostCategoryItem} {@documentEvent}", nameof(HandleAsync), createdCostCategoryItem, documentEvent);
                    }

                    var existingCostAllocation = await dbContext.Set<CostAllocation>()
                        .FirstOrDefaultAsync(e => e.PaymentVoucherId == documentEvent.Ref_Key, ct);


                    if (existingCostAllocation is null)
                    {
                        var createdCostAllocation = dbContext.Set<CostAllocation>().Add(new CostAllocation
                        {
                            IsAllocated = false,
                            IsDeleted = documentEvent.DeletionMark || !documentEvent.Posted,
                            DeletedAt = documentEvent.DeletionMark || !documentEvent.Posted ? DateTime.UtcNow : null,
                            PaymentVoucherId = documentEvent.Ref_Key,
                            PaymentVoucherType = PaymentVoucherType.Bank,
                            Number = documentEvent.Number,
                            Date = documentEvent.Date,
                            TotalAmount = documentEvent.СуммаДокумента,
                            CostCategoryId = documentEvent.КСЗ_КатегорияЗатрат_Key == Guid.Empty ? null : documentEvent.КСЗ_КатегорияЗатрат_Key,
                            CostItemId = catalog_СтатьяДДС.Ref_Key,
                            BusinessOperation = documentEvent.ХозяйственнаяОперация,
                            PaymentPurpose = documentEvent.НазначениеПлатежа,
                            AuthorId = documentEvent.Автор_Key,
                            Comment = documentEvent.Комментарий
                        }).Entity;

                        if (logger.IsEnabled(LogLevel.Debug))
                            logger.LogDebug("{Source} - createdCostAllocation: {@createdCostAllocation} {@documentEvent}", nameof(HandleAsync), createdCostAllocation, documentEvent);
                    }
                    else
                    {
                        //existingCostAllocation.IsAllocated = false;
                        existingCostAllocation.IsDeleted = documentEvent.DeletionMark || !documentEvent.Posted;
                        existingCostAllocation.DeletedAt = documentEvent.DeletionMark || !documentEvent.Posted ? DateTime.UtcNow : null;
                        existingCostAllocation.PaymentVoucherType = PaymentVoucherType.Bank;
                        existingCostAllocation.Number = documentEvent.Number;
                        existingCostAllocation.Date = documentEvent.Date;
                        existingCostAllocation.TotalAmount = documentEvent.СуммаДокумента;
                        existingCostAllocation.CostCategoryId = documentEvent.КСЗ_КатегорияЗатрат_Key == Guid.Empty ? null : documentEvent.КСЗ_КатегорияЗатрат_Key;
                        existingCostAllocation.CostItemId = catalog_СтатьяДДС.Ref_Key;
                        existingCostAllocation.BusinessOperation = documentEvent.ХозяйственнаяОперация;
                        existingCostAllocation.PaymentPurpose = documentEvent.НазначениеПлатежа;
                        existingCostAllocation.AuthorId = documentEvent.Автор_Key;
                        existingCostAllocation.Comment = documentEvent.Комментарий;
                    }
                }
            }
        }

        await dbContext.SaveChangesAsync(ct);

        if (logger.IsEnabled(LogLevel.Debug))
            logger.LogDebug("{Source} - Ok {@documentEvent}", nameof(HandleAsync), documentEvent);
    }
}
