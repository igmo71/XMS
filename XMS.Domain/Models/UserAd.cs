using XMS.Domain.Abstractions;

namespace XMS.Domain.Models
{
    public class UserAd
    {
        public required string Sid { get; set; }
        public required string Login { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Department { get; set; }
        public string? Manager { get; set; }
        public string? DistinguishedName { get; set; }
        public bool Enabled { get; set; }
    }
}
