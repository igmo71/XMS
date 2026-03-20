using XMS.Domain.Models;
using XMS.Integration.OneC.Abstractions;
using XMS.Integration.OneC.Ut.Abstractions;
using XMS.Integration.OneC.Ut.Models;

namespace XMS.Integration.OneC.Ut
{
    internal class UtService(
        UtClient utClient,
        ICatalog_СтатьиДвиженияДенежныхСредств_Service catalog_СтатьиДвиженияДенежныхСредств_Service,
        IDocument_СписаниеБезналичныхДенежныхСредств_Service document_СписаниеБезналичныхДенежныхСредств_Service) : IOneCUtService
    {
        public async Task<IReadOnlyList<UserUt>> FetchUserUtListAsync(CancellationToken ct = default)
        {
            var rootObject = await utClient.GetValueAsync<RootObject<Catalog_Пользователи>>(Catalog_Пользователи.Uri, ct);

            var result = rootObject?.Value?.Select(x => new UserUt
            {
                Id = x.Ref_Key,
                Name = x.Description ?? string.Empty,
                IsDeleted = x.DeletionMark
            }).ToList();

            return result ?? [];
        }

        public Task<IReadOnlyList<AccumulationRegister_ТоварыНаСкладах_Balance>> GetAccumulationRegister_ТоварыНаСкладах_Balance_Async(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Catalog_СтатьиДвиженияДенежныхСредств>> GetCatalog_СтатьиДвиженияДенежныхСредств_Async(CatalogQueryParameters parameters, CancellationToken ct = default)
        {
            return await catalog_СтатьиДвиженияДенежныхСредств_Service.GetListAsync(parameters, ct);    
        }

        public async Task<Catalog_СтатьиДвиженияДенежныхСредств?> GetCatalog_СтатьиДвиженияДенежныхСредств_Async(Guid refKey, CancellationToken ct = default)
        {
            return await catalog_СтатьиДвиженияДенежныхСредств_Service.GetAsync(refKey, ct);
        }

        public async Task<IReadOnlyList<Document_СписаниеБезналичныхДенежныхСредств>> GetDocument_СписаниеБезналичныхДенежныхСредств_Async(DocumentQueryParameters parameters, CancellationToken ct = default)
        {
            return await document_СписаниеБезналичныхДенежныхСредств_Service.GetListAsync(parameters, ct);
        }

        public async Task<Document_СписаниеБезналичныхДенежныхСредств?> GetDocument_СписаниеБезналичныхДенежныхСредств_Async(Guid refKey, CancellationToken ct = default)
        {
            return await document_СписаниеБезналичныхДенежныхСредств_Service.GetAsync(refKey, ct);
        }
    }
}
