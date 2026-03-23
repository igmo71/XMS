using System.ComponentModel.DataAnnotations;
using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_СтатьиДвиженияДенежныхСредств_Feature
{
    public class Catalog_СтатьиДвиженияДенежныхСредств: IOneCCatalog
    {
        public Guid Ref_Key { get; set; }
        [MaxLength(OneCSettings.CODE)] public string? DataVersion { get; set; }
        [MaxLength(OneCSettings.DESCRIPTION)] public string? Description { get; set; }
        public bool DeletionMark { get; set; }
        [MaxLength(OneCSettings.CODE)] public string? Code { get; set; }
        public Guid? Parent_Key { get; set; }
        public bool IsFolder { get; set; }

        public static string Uri => "Catalog_СтатьиДвиженияДенежныхСредств?$format=json&$select=Ref_Key,Description,DeletionMark,Code,Parent_Key,IsFolder&$inlinecount=allpages";

        public static string GetUriByRefKey(Guid refKey)
        {
            string uri = $"{Uri}&$filter=Ref_Key eq guid'{refKey}'";

            return uri;
        }

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
