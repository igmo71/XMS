using XMS.Web.Core.Abstractions;

namespace XMS.Web.Modules.Employees.Domain
{
    public class UserAd : IHasName
    {
        public string Sid { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Login { get; set; }
        public string? Title { get; set; }
        public string? Department { get; set; }
        public string? Manager { get; set; }
        public string? DistinguishedName { get; set; }
        public bool Enabled { get; set; }
    }
}
