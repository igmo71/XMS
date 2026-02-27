namespace XMS.Modules.GodooModule.Infrastructure.OneS.Buh.Domain
{
    public class InformationRegister_НоменклатураМаркетплейсов
    {
        public required string ИдентификаторТовара { get; set; }
        public string? Штрихкод { get; set; }
        public required string Маркетплейс { get; set; }
        public required string Номенклатура_Key { get; set; }
        public string? Организация_Key { get; set; }

        public static string Uri => "InformationRegister_НоменклатураМаркетплейсов?$format=json&$inlinecount=allpages";
    }
}
