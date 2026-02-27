namespace XMS.Modules.GodooModule.Domain
{
    public class InformationRegister_НоменклатураМаркетплейсов
    {
        public string? ИдентификаторТовара { get; set; }
        public string? Штрихкод { get; set; }
        public string? Маркетплейс { get; set; }
        public string? Номенклатура_Key { get; set; }
        public string? Организация_Key { get; set; }

        public static string Uri => "InformationRegister_НоменклатураМаркетплейсов?$format=json&$inlinecount=allpages";
    }
}
