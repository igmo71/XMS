using XMS.Modules.GodooModule.Domain;

namespace XMS.Modules.GodooModule.Application.Mapping;

public static class ProductIdMap
{
    public static string? From(YunuMarketplaceRelation yunuRelation)
    {
        return yunuRelation.Marketplace switch
        {
            "ozon" or "yandex_market" or "mega_market" => yunuRelation.OfferId,
            "wildberries" => yunuRelation.VendorCode,
            _ => null,
        };
    }
}
