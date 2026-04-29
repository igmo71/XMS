using XMS.Application.Common;

namespace XMS.Integrations.OneC.Common;

public record struct CatalogQueryParameters(
    string? SearchTerm = null,
    bool IncludeDeleted = false,
    int? Skip = AppSettings.Default.Skip,
    int? Take = AppSettings.Default.Take);

public record struct DocumentQueryParameters(
    string? SearchTerm = null,
    DateTime? From = null,
    DateTime? To = null,
    int? Skip = AppSettings.Default.Skip,
    int? Take = AppSettings.Default.Take);
