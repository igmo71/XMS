using XMS.Integration.OneC.Ut.Features.Catalog_КСЗ_КатегорииСтатейЗатрат_Feature;

namespace XMS.Integration.OneC.Ut.Abstractions;

internal interface ICatalog_КСЗ_КатегорииСтатейЗатрат_Service
{
    Task Create(Catalog_КСЗ_КатегорииСтатейЗатрат item);
    Task Update(Catalog_КСЗ_КатегорииСтатейЗатрат item);
    Task MarkDelete(Catalog_КСЗ_КатегорииСтатейЗатрат item);
}
