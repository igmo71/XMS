namespace XMS.Infrastructure.Integration.OneS
{
    internal abstract class OneSClientConfig
    {
        public required string BaseAddress { get; set; }
        public required string ServiceUri { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
