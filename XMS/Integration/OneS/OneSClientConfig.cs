namespace XMS.Integration.OneS
{
    public abstract class OneSClientConfig
    {
        public required string BaseAddress { get; set; }
        public required string ServiceUri { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
