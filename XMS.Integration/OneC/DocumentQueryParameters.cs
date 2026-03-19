using XMS.Core;

namespace XMS.Integration.OneC
{
    public record DocumentQueryParameters(
        string? NumberTerm = null,
        DateTime? From = null,
        DateTime? To = null,
        int? Skip = AppSettings.Default.Skip,
        int? Take = AppSettings.Default.Take);
}
