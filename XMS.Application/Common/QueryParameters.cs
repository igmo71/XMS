namespace XMS.Application.Common;

public record QueryParameters(
    int? Skip = AppSettings.Default.Skip,
    int? Take = AppSettings.Default.Take,
    bool? IncludeDeleted = false);
