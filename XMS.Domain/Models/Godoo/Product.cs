namespace XMS.Domain.Models.Godoo
{
    public class Product
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Sku { get; set; }
        public bool IsFolder { get; set; }
    }
}
