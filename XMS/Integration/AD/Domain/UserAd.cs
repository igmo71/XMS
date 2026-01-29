using System.ComponentModel.DataAnnotations;

namespace XMS.Integration.AD.Domain
{
    public class UserAd
    {
        public string? Sid { get; set; }
        public string? Name { get; set; }
        public string? Login { get; set; }
        public string? Title { get; set; }
        public string? Department { get; set; }
        public string? Manager { get; set; }
        public string? DistinguishedName { get; set; }
        public bool Enabled { get; set; }
    }
}
