using Microsoft.EntityFrameworkCore;
using XMS.Modules.CostModule.Domain;

namespace XMS.Modules.CostModule.Integration;

internal class Document_РасходныйКассовыйОрдер_ReceivedHandler(IDbContextFactoryProxy dbFactory)
    : IAppEventHandler<Document_РасходныйКассовыйОрдер_Received>
{
    public async Task HandleAsync(Document_РасходныйКассовыйОрдер_Received received, CancellationToken ct = default)
    {
        using var dbContext = dbFactory.CreateDbContext();

        var existing = await dbContext.Set<CostAllocation>().FirstOrDefaultAsync(e => e.PaymentVoucherId == received.Ref_Key, ct);

        if (existing is null)
        {
            var created = new CostAllocation
            {
                IsAllocated = false,
                PaymentVoucherId = received.Ref_Key,
                PaymentVoucherType = PaymentVoucherType.Cash,
                Number = received.Number,
                Date = received.Date,
                TotalAmount = received.СуммаДокумента,
                CostCategoryId = received.КСЗ_КатегорияЗатрат_Key == Guid.Empty ? null : received.КСЗ_КатегорияЗатрат_Key,
                Catalog_СтатьяДДС_Key = received.СтатьяДвиженияДенежныхСредств_Key == Guid.Empty ? null : received.СтатьяДвиженияДенежныхСредств_Key,
                BusinessOperation = received.ХозяйственнаяОперация,
                AuthorId = received.Автор_Key,
                Comment = received.Комментарий
            };

            await dbContext.Set<CostAllocation>().AddAsync(created, ct);
        }
        else
        {
            existing.IsAllocated = false;
            existing.PaymentVoucherType = PaymentVoucherType.Cash;
            existing.Number = received.Number;
            existing.Date = received.Date;
            existing.TotalAmount = received.СуммаДокумента;
            existing.PaymentPurpose = received.ХозяйственнаяОперация;
            existing.CostCategoryId = received.КСЗ_КатегорияЗатрат_Key == Guid.Empty ? null : received.КСЗ_КатегорияЗатрат_Key;
            existing.Catalog_СтатьяДДС_Key = received.СтатьяДвиженияДенежныхСредств_Key == Guid.Empty ? null : received.СтатьяДвиженияДенежныхСредств_Key;
            existing.BusinessOperation = received.ХозяйственнаяОперация;
            existing.AuthorId = received.Автор_Key;
            existing.Comment = received.Комментарий;
        }

        await dbContext.SaveChangesAsync(ct);
    }
}
