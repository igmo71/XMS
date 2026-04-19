using System.Text.Json.Serialization;

namespace XMS.Modules.GodooModule.Domain;

/// <summary>
/// Соответствие артикулов и штрихкодов Yunu и маркетплейсов
/// </summary>
public class YunuArticleRelation
{
    [JsonPropertyName("status")] public string? Status { get; set; }
    [JsonPropertyName("result")] public YunuProduct[]? Products { get; set; }
}

public class YunuProduct
{
    [JsonPropertyName("product_id")] public int ProductId { get; set; }
    [JsonPropertyName("product_name")] public string? ProductName { get; set; }
    [JsonPropertyName("yunu_article")] public string? YunuArticle { get; set; }
    [JsonPropertyName("marketplace_relations")] public YunuMarketplaceRelation[]? MarketplaceRelations { get; set; }
    [JsonPropertyName("barcodes")] public string[]? Barcodes { get; set; }
    [JsonPropertyName("child_products")] public ChildProduct[]? ChildProducts { get; set; }
}

public class YunuMarketplaceRelation
{
    [JsonPropertyName("cabinet_id")] public int CabinetId { get; set; }
    [JsonPropertyName("barcode")] public string? Barcode { get; set; }
    [JsonPropertyName("marketplace")] public string? Marketplace { get; set; }

    // ozon, yandex_market, mega_market
    [JsonConverter(typeof(FlexibleStringConverter))]
    [JsonPropertyName("offer_id")] public string? OfferId { get; set; }
    [JsonPropertyName("sku")] public string? Sku { get; set; }

    // wildberries
    [JsonPropertyName("vendor_code")] public string? VendorCode { get; set; }
    [JsonPropertyName("nm_id")] public string? NmId { get; set; }
}

public class ChildProduct
{
    [JsonPropertyName("product_id")] public int ProductId { get; set; }
    [JsonPropertyName("count")] public int Count { get; set; }
}
