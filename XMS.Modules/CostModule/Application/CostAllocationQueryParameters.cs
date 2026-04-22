using XMS.Application.Common;

namespace XMS.Modules.CostModule.Application;

public record struct CostAllocationQueryParameters(
    int? Skip = AppSettings.Default.Skip,
    int? Take = AppSettings.Default.Take,
    bool? IncludeDeleted = false,
    string? SearchTerm = null,
    DateTime? From = null,
    DateTime? To = null,
    int Type = -1);