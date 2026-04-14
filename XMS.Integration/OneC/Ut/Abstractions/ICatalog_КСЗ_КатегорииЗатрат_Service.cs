using XMS.Integration.OneC.Ut.Features.Catalog_КСЗ_КатегорииЗатрат_Feature;

namespace XMS.Integration.OneC.Ut.Abstractions;

internal interface ICatalog_КСЗ_КатегорииЗатрат_Service
{
    Task Create(Catalog_КСЗ_КатегорииЗатрат item);
    Task Update(Catalog_КСЗ_КатегорииЗатрат item);
    Task MarkDelete(Catalog_КСЗ_КатегорииЗатрат item);
}
