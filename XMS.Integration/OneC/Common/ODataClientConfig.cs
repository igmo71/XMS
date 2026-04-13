namespace XMS.Integration.OneC.Common;

public abstract class ODataClientConfig
{
    public required string BaseAddress { get; set; }
    public required string ServiceUri { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
}
