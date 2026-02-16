namespace XMS.Infrastructure.Integration.OneS.Ut.Domain
{
    internal class Catalog_СтатьиДвиженияДенежныхСредств
    {
        public Guid Ref_Key { get; set; }
        public string? Description { get; set; }
        public bool DeletionMark { get; set; }
        public string? Code { get; set; }
        public Guid? Parent_Key { get; set; }
        public bool IsFolder { get; set; }

        public static string Uri => "Catalog_СтатьиДвиженияДенежныхСредств?$format=json&$select=Ref_Key,Description,DeletionMark,Code,Parent_Key,IsFolder&$inlinecount=allpages";

        //public string DataVersion { get; set; }
        //public string КорреспондирующийСчет { get; set; }
        //public string Описание { get; set; }
        //public string ВидДвиженияДенежныхСредств { get; set; }
        //public string РеквизитДопУпорядочивания { get; set; }
        //public string ПриоритетОплаты_Key { get; set; }
        //public bool? НеУчитываетсяВНалоговойБазеАУСН { get; set; }
        //public string НаименованиеЯзык1 { get; set; }
        //public string НаименованиеЯзык2 { get; set; }
        //public Хозяйственныеоперации[] ХозяйственныеОперации { get; set; }
        //public object[] ДополнительныеРеквизиты { get; set; }
        //public bool Predefined { get; set; }
        //public string PredefinedDataName { get; set; }
        //public string ParentnavigationLinkUrl { get; set; }
    }

    //public class Хозяйственныеоперации
    //{
    //    public string Ref_Key { get; set; }
    //    public string LineNumber { get; set; }
    //    public string ХозяйственнаяОперация { get; set; }
    //}
}
