using Microsoft.Extensions.Hosting;
using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Document_РеализацияТоваровУслуг_Feature;

public class Document_РеализацияТоваровУслуг : IDocument
{
    public Guid Ref_Key { get; set; }
    public string? DataVersion { get; set; }
    public bool DeletionMark { get; set; }
    public string? Number { get; set; }
    public DateTime Date { get; set; }
    public bool Posted { get; set; }
    public decimal СуммаДокумента { get; set; }
    public string? ХозяйственнаяОперация { get; set; }
    public Guid? Партнер_Key { get; set; }
    public Guid? Склад_Key { get; set; }

    public static string Uri => "Document_РеализацияТоваровУслуг?$format=json&$inlinecount=allpages" +
        "&$select=Ref_Key,DataVersion,DeletionMark,Number,Date,Posted,СуммаДокумента,ХозяйственнаяОперация,Партнер_Key,Склад_Key";

    public static string GetUriByRefKey(Guid refKey) => $"{Uri}&$filter=Ref_Key eq guid'{refKey}'";

    public static string GetUriByDate(DateTime? from = null, DateTime? to = null) =>
        $"{Uri}&$filter=DeletionMark eq false and Posted eq true and Date ge datetime'{from:s}' and Date lt datetime'{to:s}'";

    public static string GetExchangeName(IHostEnvironment hostEnvironment) =>
        hostEnvironment.IsDevelopment() ? $"dev_{nameof(Document_РеализацияТоваровУслуг)}" : $"{nameof(Document_РеализацияТоваровУслуг)}";

    public static string GetQueueName(IHostEnvironment hostEnvironment) =>
        hostEnvironment.IsDevelopment() ? $"dev_{nameof(Document_РеализацияТоваровУслуг)}" : $"xms_{nameof(Document_РеализацияТоваровУслуг)}";

    //public string АдресДоставки { get; set; }
    //public string БанковскийСчетОрганизации_Key { get; set; }
    //public string БанковскийСчетКонтрагента_Key { get; set; }
    //public string БанковскийСчетГрузоотправителя_Key { get; set; }
    //public string БанковскийСчетГрузополучателя_Key { get; set; }
    //public string Валюта_Key { get; set; }
    //public string ВалютаВзаиморасчетов_Key { get; set; }
    //public string Грузоотправитель_Key { get; set; }
    //public string Грузополучатель_Key { get; set; }
    //public string ДоверенностьВыдана { get; set; }
    //public DateTime ДоверенностьДата { get; set; }
    //public string ДоверенностьЛицо { get; set; }
    //public string ДоверенностьНомер { get; set; }
    //public string ЗаказКлиента { get; set; }
    //public string ЗаказКлиента_Type { get; set; }
    //public string Организация_Key { get; set; }
    //public string Контрагент_Key { get; set; }
    //public string Менеджер_Key { get; set; }
    //public string НалогообложениеНДС { get; set; }
    //public string Подразделение_Key { get; set; }
    //public string Сделка_Key { get; set; }
    //public bool СкидкиРассчитаны { get; set; }
    //public bool Согласован { get; set; }
    //public string Соглашение_Key { get; set; }
    //public int СуммаВзаиморасчетов { get; set; }
    //public string Комментарий { get; set; }
    //public string ФормаОплаты { get; set; }
    //public bool ЦенаВключаетНДС { get; set; }
    //public string Касса_Key { get; set; }
    //public string Отпустил_Key { get; set; }
    //public string ОтпустилДолжность { get; set; }
    //public bool РеализацияПоЗаказам { get; set; }
    //public string ГруппаФинансовогоУчета_Key { get; set; }
    //public string КартаЛояльности_Key { get; set; }
    //public string Договор_Key { get; set; }
    //public string Основание { get; set; }
    //public string Статус { get; set; }
    //public string Автор_Key { get; set; }
    //public string СпособДоставки { get; set; }
    //public string ЗонаДоставки_Key { get; set; }
    //public string АдресДоставкиЗначенияПолей { get; set; }
    //public string ПеревозчикПартнер_Key { get; set; }
    //public DateTime ВремяДоставкиС { get; set; }
    //public DateTime ВремяДоставкиПо { get; set; }
    //public string АдресДоставкиПеревозчика { get; set; }
    //public string АдресДоставкиПеревозчикаЗначенияПолей { get; set; }
    //public string ДополнительнаяИнформацияПоДоставке { get; set; }
    //public string КонтактноеЛицо_Key { get; set; }
    //public string Руководитель_Key { get; set; }
    //public string ГлавныйБухгалтер_Key { get; set; }
    //public string ПорядокРасчетов { get; set; }
    //public bool ВернутьМногооборотнуюТару { get; set; }
    //public DateTime ДатаВозвратаМногооборотнойТары { get; set; }
    //public string СостояниеЗаполненияМногооборотнойТары { get; set; }
    //public bool ВидыЗапасовУказаныВручную { get; set; }
    //public bool ТребуетсяЗалогЗаТару { get; set; }
    //public DateTime ОснованиеДата { get; set; }
    //public string ОснованиеНомер { get; set; }
    //public string ДопоставкаПоРеализации { get; set; }
    //public string ДопоставкаПоРеализации_Type { get; set; }
    //public DateTime ДатаПереходаПраваСобственности { get; set; }
    //public string ВариантОформленияПродажи { get; set; }
    //public string ИдентификаторПлатежа { get; set; }
    //public bool ОсобыеУсловияПеревозки { get; set; }
    //public string ОсобыеУсловияПеревозкиОписание { get; set; }
    //public string НаправлениеДеятельности_Key { get; set; }
    //public int КурсЧислитель { get; set; }
    //public int КурсЗнаменатель { get; set; }
    //public bool ЕстьМаркируемаяПродукцияГИСМ { get; set; }
    //public int СуммаВзаиморасчетовПоТаре { get; set; }
    //public bool ОплатаВВалюте { get; set; }
    //public string Курьер_Key { get; set; }
    //public string Сборщик_Key { get; set; }
    //public string АдресДоставкиЗначение { get; set; }
    //public string АдресДоставкиПеревозчикаЗначение { get; set; }
    //public string ВариантВыбытияМаркируемойПродукции { get; set; }
    //public string КлиентКонтрагент_Key { get; set; }
    //public string КлиентПартнер_Key { get; set; }
    //public string КлиентДоговор_Key { get; set; }
    //public string ЭтапГосконтрактаЕИС { get; set; }
    //public string ОбъектРасчетовУпр_Key { get; set; }
    //public string ГрафикОплаты_Key { get; set; }
    //public string СопроводительныеДокументы { get; set; }
    //public string СведенияОТранспортировкеИГрузе { get; set; }
    //public string КодСпециальныхОбстоятельств { get; set; }
    //public string СтранаРеализации_Key { get; set; }
    //public string НомерКомиссионера { get; set; }
    //public DateTime ДатаКомиссионера { get; set; }
    //public string НаименованиеДокументаКомиссионера { get; set; }
    //public DateTime ИШ_ЖелаемаяДатаДоставки { get; set; }
    //public string ИШ_ДоставкаКонтактноеЛицо { get; set; }
    //public string ИШ_ДоставкаТелефон { get; set; }
    //public string ИШ_ТипВыгрузки { get; set; }
    //public DateTime ИШ_НачалоОбеда { get; set; }
    //public DateTime ИШ_КонецОбеда { get; set; }
    //public bool САР_Корректировка { get; set; }
    //public object[] Товары { get; set; }
    //public object[] СкидкиНаценки { get; set; }
    //public object[] ВидыЗапасов { get; set; }
    //public object[] ДополнительныеРеквизиты { get; set; }
    //public object[] РасшифровкаПлатежа { get; set; }
    //public object[] Серии { get; set; }
    //public object[] ЭтапыГрафикаОплаты { get; set; }
    //public object[] ШтрихкодыУпаковок { get; set; }
    //public object[] НачислениеБонусныхБаллов { get; set; }
    //public string ОрганизацияnavigationLinkUrl { get; set; }
    //public string ПартнерnavigationLinkUrl { get; set; }
    //public string ПодразделениеnavigationLinkUrl { get; set; }
    //public string СоглашениеnavigationLinkUrl { get; set; }
}

