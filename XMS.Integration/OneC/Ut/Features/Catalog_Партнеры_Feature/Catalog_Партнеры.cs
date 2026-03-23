using System.ComponentModel.DataAnnotations;
using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Партнеры_Feature
{
    public class Catalog_Партнеры : IOneCCatalog
    {
        public Guid Ref_Key { get; set; }
        [MaxLength(OneCSettings.CODE)] public string? DataVersion { get; set; }
        public bool DeletionMark { get; set; }
        public Guid? Parent_Key { get; set; }
        [MaxLength(OneCSettings.DESCRIPTION)] public string? Description { get; set; }
        public Guid? ОсновнойМенеджер_Key { get; set; }
        public Guid? БизнесРегион_Key { get; set; }
        public DateTime ДатаРегистрации { get; set; }
        [MaxLength(OneCSettings.CODE)] public string? ЮрФизЛицо { get; set; }
        [MaxLength(OneCSettings.COMMENT)] public string? Комментарий { get; set; }
        //[MaxLength(OneCSettings.DESCRIPTION)] public string? НаименованиеПолное { get; set; }
        //[MaxLength(OneCSettings.DESCRIPTION)] public string? ДополнительнаяИнформация { get; set; }
        public bool Клиент { get; set; }
        public bool Поставщик { get; set; }
        public bool Конкурент { get; set; }
        public bool Перевозчик { get; set; }
        public bool ПрочиеОтношения { get; set; }
        //public bool ОбслуживаетсяТорговымиПредставителями { get; set; }

        public static string Uri => "Catalog_Партнеры?$format=json" +
            "&$select=Ref_Key,DataVersion,DeletionMark,Parent_Key,Description,ОсновнойМенеджер_Key,БизнесРегион_Key,ДатаРегистрации,ЮрФизЛицо,Комментарий,НаименованиеПолное,ДополнительнаяИнформация,Клиент,Поставщик,Конкурент,Перевозчик,ПрочиеОтношения" +
            "&$inlinecount=allpages";

        public static string GetUriByRefKey(Guid refKey)
        {
            string uri = $"{Uri}&$filter=Ref_Key eq guid'{refKey}'";

            return uri;
        }

        //[MaxLength(OneCSettings.CODE)] public string? Code { get; set; }
        //public string ГруппаДоступа_Key { get; set; }
        //public string ШаблонЭтикетки_Key { get; set; }
        //public string Пол { get; set; }
        //public DateTime ДатаРождения { get; set; }
        //public string НазначениеПереработчика_Key { get; set; }
        //public string ВариантОтправкиЭлектронногоЧека { get; set; }
        //public string ЗонаДоставки_Key { get; set; }
        //public string ВидЦен_Key { get; set; }
        //public string ИндивидуальныйВидЦены_Key { get; set; }
        //public string КомментарийЯзык1 { get; set; }
        //public string КомментарийЯзык2 { get; set; }
        //public string НаименованиеЯзык1 { get; set; }
        //public string НаименованиеЯзык2 { get; set; }
        //public object[] ДополнительныеРеквизиты { get; set; }
        //public Контактнаяинформация[] КонтактнаяИнформация { get; set; }
        //public ВДП_Видыдеятельности[] ВДП_ВидыДеятельности { get; set; }
        //public bool Predefined { get; set; }
        //public string PredefinedDataName { get; set; }
        //public string БизнесРегионnavigationLinkUrl { get; set; }
        //public string ГруппаДоступаnavigationLinkUrl { get; set; }
        //public string ОсновнойМенеджерnavigationLinkUrl { get; set; }
        //public string ЗонаДоставкиnavigationLinkUrl { get; set; }
    }

    //public class Контактнаяинформация
    //{
    //    public string Ref_Key { get; set; }
    //    public string LineNumber { get; set; }
    //    public string Тип { get; set; }
    //    public string Вид_Key { get; set; }
    //    public string Представление { get; set; }
    //    public string ЗначенияПолей { get; set; }
    //    public string Страна { get; set; }
    //    public string Регион { get; set; }
    //    public string Город { get; set; }
    //    public string АдресЭП { get; set; }
    //    public string ДоменноеИмяСервера { get; set; }
    //    public string НомерТелефона { get; set; }
    //    public string НомерТелефонаБезКодов { get; set; }
    //    public string ВидДляСписка_Key { get; set; }
    //    public string Значение { get; set; }
    //}

    //public class ВДП_Видыдеятельности
    //{
    //    public string Ref_Key { get; set; }
    //    public string LineNumber { get; set; }
    //    public string ВидДеятельности_Key { get; set; }
    //}
}
