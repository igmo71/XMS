using XMS.Common.SharedKernel.Abstractions;

namespace XMS.Common.SharedKernel;

public sealed record Error(int Id, string Name, string? Description = null) : IState
{
    public IState WithDescription(string description) => this with { Description = description };

    public static readonly Error None = new(0, string.Empty);
    public static readonly Error Unauthorized = new(401, "Unauthorized");
    public static readonly Error AccessDenied = new(403, "Forbidden", "Access Denied");
    public static readonly Error NotFound = new(404, "NotFound");
    public static readonly Error UnknownError = new(520, "UnknownError");
    public static readonly Error CloneFail = new(530, "CloneFail");
    public static readonly Error GuidParseFail = new(531, "GuidParseFail");
    public static readonly Error IsNullOrWhiteSpace = new(532, "IsNullOrWhiteSpace");
}
