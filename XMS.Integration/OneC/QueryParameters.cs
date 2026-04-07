using XMS.Core;

namespace XMS.Integration.OneC;

public record struct CatalogQueryParameters(
    string? DescriptionTerm = null,
    bool IncludeDeleted = false,
    int? Skip = AppSettings.Default.Skip,
    int? Take = AppSettings.Default.Take);

public record struct DocumentQueryParameters(
    string? NumberTerm = null,
    DateTime? From = null,
    DateTime? To = null,
    int? Skip = AppSettings.Default.Skip,
    int? Take = AppSettings.Default.Take);
