namespace XMS.Modules.GodooModule.Domain
{
    public class MarketplaceRelation
    {
        public required string Marketplace { get; set; }
        public required string MarketplaceSku { get; set; }
        public string? Barcode { get; set; }
        public required string ProductId { get; set; }
        public string? CompanyId { get; set; }
    }
}
