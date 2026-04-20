using Microsoft.EntityFrameworkCore;
using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Integration;

internal class Document_СписаниеБезналичныхДенежныхСредств_Handler(IDbContextFactoryProxy dbFactory)
    : IAppEventHandler<Document_СписаниеБезналичныхДенежныхСредств>
{
    public async Task HandleAsync(Document_СписаниеБезналичныхДенежныхСредств documentEvent, CancellationToken ct = default)
    {
        using var dbContext = dbFactory.CreateDbContext();

        var existing = await dbContext.Set<CostAllocation>()
            .FirstOrDefaultAsync(e => e.PaymentVoucherId == documentEvent.Ref_Key, ct);

        if (existing is null)
        {
            var created = new CostAllocation
            {
                PaymentVoucherId = documentEvent.Ref_Key,
                PaymentVoucherType = PaymentVoucherType.Bank,
                IsAllocated = false,
                Number = documentEvent.Number,
                Date = documentEvent.Date,
                TotalAmount = documentEvent.СуммаДокумента,
                CostCategoryId = documentEvent.КСЗ_КатегорияЗатрат_Key == Guid.Empty ? null : documentEvent.КСЗ_КатегорияЗатрат_Key,
                Catalog_СтатьяДДС_Key = documentEvent.СтатьяДвиженияДенежныхСредств_Key == Guid.Empty ? null : documentEvent.СтатьяДвиженияДенежныхСредств_Key,
                BusinessOperation = documentEvent.ХозяйственнаяОперация,
                PaymentPurpose = documentEvent.НазначениеПлатежа,
                AuthorId = documentEvent.Автор_Key,
                Comment = documentEvent.Комментарий
            };

            await dbContext.Set<CostAllocation>().AddAsync(created, ct);
        }
        else
        {
            existing.IsDeleted = documentEvent.DeletionMark;
            existing.DeletedAt = DateTime.UtcNow;
            existing.PaymentVoucherType = PaymentVoucherType.Bank;
            existing.IsAllocated = false;
            existing.Number = documentEvent.Number;
            existing.Date = documentEvent.Date;
            existing.TotalAmount = documentEvent.СуммаДокумента;
            existing.PaymentPurpose = documentEvent.НазначениеПлатежа;
            existing.CostCategoryId = documentEvent.КСЗ_КатегорияЗатрат_Key == Guid.Empty ? null : documentEvent.КСЗ_КатегорияЗатрат_Key;
            existing.Catalog_СтатьяДДС_Key = documentEvent.СтатьяДвиженияДенежныхСредств_Key == Guid.Empty ? null : documentEvent.СтатьяДвиженияДенежныхСредств_Key;
            existing.BusinessOperation = documentEvent.ХозяйственнаяОперация;
            existing.AuthorId = documentEvent.Автор_Key;
            existing.Comment = documentEvent.Комментарий;
        }

        await dbContext.SaveChangesAsync(ct);
    }
}
