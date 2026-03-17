using XMS.Application.Abstractions;
using XMS.Modules.CostModule.Abstractions;
using XMS.Modules.CostModule.Infrastructure.OneS.Models;

namespace XMS.Modules.CostModule.Application
{
    internal class CashOutlayRequestService(ICostUtService utService, IDbContextFactoryProxy dbFactory) : ICashOutlayRequestService
    {
        public Task<IReadOnlyList<Document_ЗаявкаНаРасходованиеДенежныхСредств>?> GetCashOutlayRequestByDateAsync(DateTime? begin = null, DateTime? end = null, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Document_ЗаявкаНаРасходованиеДенежныхСредств?> GetCashOutlayRequestByRefKeyAsync(string refKey, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
