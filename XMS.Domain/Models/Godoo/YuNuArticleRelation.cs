using System.Text.Json.Serialization;

namespace XMS.Domain.Models.Godoo
{
    public class YuNuArticleRelation
    {
        [JsonPropertyName("status")] public string? Status { get; set; }
        [JsonPropertyName("result")] public Result[]? Result { get; set; }
    }

    public class Result
    {
        [JsonPropertyName("product_id")] public int ProductId { get; set; }
        [JsonPropertyName("product_name")] public string? ProductName { get; set; }
        [JsonPropertyName("yunu_article")] public string? YuNuArticle { get; set; }
        [JsonPropertyName("marketplace_relations")] public YuNuMarketplaceRelation[]? MarketplaceRelations { get; set; }
        [JsonPropertyName("barcodes")] public string[]? Barcodes { get; set; }
        [JsonPropertyName("child_products")] public ChildProduct[]? ChildProducts { get; set; }
    }

    public class YuNuMarketplaceRelation
    {
        [JsonPropertyName("cabinet_id")] public int CabinetId { get; set; }
        [JsonPropertyName("marketplace")] public string? Marketplace { get; set; }
        [JsonPropertyName("offer_id")] public object? OfferId { get; set; } // string and int ???
        [JsonPropertyName("sku")] public string? Sku { get; set; }
        [JsonPropertyName("barcode")] public string? Barcode { get; set; }
        [JsonPropertyName("nm_id")] public string? NmId { get; set; }
        [JsonPropertyName("vendor_code")] public string? VendorCode { get; set; }
    }

    public class ChildProduct
    {
        [JsonPropertyName("product_id")] public int ProductId { get; set; }
        [JsonPropertyName("count")] public int Count { get; set; }
    }

}
