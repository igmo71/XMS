using XMS.Modules.GodooModule.Application;

namespace XMS.Modules.GodooModule.Domain;

public class InformationRegister_НоменклатураМаркетплейсов
{
    public string? ИдентификаторТовара { get; set; }
    public string? Штрихкод { get; set; }
    public string? Маркетплейс { get; set; }
    public string? Номенклатура_Key { get; set; }
    public string? Организация_Key { get; set; }

    public static string Uri => $"InformationRegister_НоменклатураМаркетплейсов?$format=json&$inlinecount=allpages&$filter=ИдентификаторТовара eq '100000009051' and Маркетплейс eq 'Мегамаркет' and Штрихкод eq '' and Номенклатура_Key eq guid'd83087f2-1906-11ef-ba82-00155d01d111' and Организация_Key eq guid'246b903a-2be8-11e7-80d7-0cc47adeb012'";

    internal static string GetUri(GodooMarketplaceRelation godooMarketplaceRelation)
    {
        string uri = $"InformationRegister_НоменклатураМаркетплейсов?$format=json&$inlinecount=allpages" +
            $"&$filter=ИдентификаторТовара eq '{godooMarketplaceRelation.YunuProductId}' " +
            $"and Маркетплейс eq '{godooMarketplaceRelation.Marketplace}' " +
            $"and Штрихкод eq '{godooMarketplaceRelation.Barcode}' " +
            $"and Номенклатура_Key eq guid'{godooMarketplaceRelation.OneSProductKey}' " +
            $"and Организация_Key eq guid'{godooMarketplaceRelation.CompanyKey}'";
        return uri;
    }
}
