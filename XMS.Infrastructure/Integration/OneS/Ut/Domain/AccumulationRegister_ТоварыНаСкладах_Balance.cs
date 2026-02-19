namespace XMS.Infrastructure.Integration.OneS.Ut.Domain
{
    internal class AccumulationRegister_ТоварыНаСкладах_Balance
    {
        public Guid Номенклатура_Key { get; set; }
        public Guid Склад_Key { get; set; }
        public decimal ВНаличииBalance { get; set; }

        public static string Uri => "AccumulationRegister_ТоварыНаСкладах/Balance?$format=json&$select=Номенклатура_Key,Склад_Key,ВНаличииBalance&$inlinecount=allpages";
    }
}
