using XMS.Core;

namespace XMS.Integration.OneC
{
    public record CatalogQueryParameters(
        string? DescriptionTerm = null,
        int? Skip = AppSettings.Default.Skip,
        int? Take = AppSettings.Default.Take)
        : PaginationParameters(Skip, Take);
}
