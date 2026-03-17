namespace XMS.Application.Common
{
    public record DocumentQueryParameters(
        string? NumberTerm = null,
        DateTime? From = null,
        DateTime? To = null);
}
