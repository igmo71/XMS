namespace XMS.Application.Common
{
    public sealed record ServiceError(int Code, string Name, string? Description = null)
    {
        public bool Is(string name) => Name == name;
        public bool Is(int id) => Code == id;

        public ServiceError WithDescription(string description) => this with { Description = description };

        public static readonly ServiceError None = new(0, string.Empty);
        public static readonly ServiceError BedRequest = new(400, nameof(BedRequest));
        public static readonly ServiceError Unauthorized = new(401, nameof(Unauthorized));
        public static readonly ServiceError Forbidden = new(403, nameof(Forbidden), "Access Denied");
        public static readonly ServiceError NotFound = new(404, nameof(NotFound));
        public static readonly ServiceError ImATeapot = new(418, nameof(ImATeapot));
        public static readonly ServiceError InternalServerError = new(500, nameof(InternalServerError));
        public static readonly ServiceError NotImplemented = new(501, nameof(NotImplemented));
        public static readonly ServiceError ServiceUnavailable = new(503, nameof(ServiceUnavailable));
        public static readonly ServiceError UnknownError = new(900, nameof(UnknownError));
        public static readonly ServiceError InvalidOperation = new(910, nameof(InvalidOperation));
    }
}
