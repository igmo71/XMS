using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Document_ЗаказКлиента_Feature;

public class Document_ЗаказКлиента : IDocument, ISyncable
{
    public static string? Select =>
        "Ref_Key,DataVersion,DeletionMark,Number,Date,Posted,СуммаДокумента,ХозяйственнаяОперация,Партнер_Key,Склад_Key";

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

    //public string Контрагент_Key { get; set; }
    //public string Организация_Key { get; set; }
    //public string Соглашение_Key { get; set; }
    //public string Сделка_Key { get; set; }
    //public string Валюта_Key { get; set; }
    //public string ГрафикОплаты_Key { get; set; }
    //public bool ЦенаВключаетНДС { get; set; }
    //public string Менеджер_Key { get; set; }
    //public string ДополнительнаяИнформация { get; set; }
    //public string ДокументОснование { get; set; }
    //public string ДокументОснование_Type { get; set; }
    //public bool НеОтгружатьЧастями { get; set; }
    //public string Статус { get; set; }
    //public string МаксимальныйКодСтроки { get; set; }
    //public DateTime ДатаСогласования { get; set; }
    //public bool Согласован { get; set; }
    //public string ФормаОплаты { get; set; }
    //public string БанковскийСчет_Key { get; set; }
    //public string БанковскийСчетКонтрагента_Key { get; set; }
    //public string Касса_Key { get; set; }
    //public int СуммаАвансаДоОбеспечения { get; set; }
    //public int СуммаПредоплатыДоОтгрузки { get; set; }
    //public DateTime ДатаОтгрузки { get; set; }
    //public string АдресДоставки { get; set; }
    //public string НалогообложениеНДС { get; set; }
    //public bool СкидкиРассчитаны { get; set; }
    //public string Комментарий { get; set; }
    //public string НомерПоДаннымКлиента { get; set; }
    //public DateTime ДатаПоДаннымКлиента { get; set; }
    //public string Грузоотправитель_Key { get; set; }
    //public string Грузополучатель_Key { get; set; }
    //public string БанковскийСчетГрузоотправителя_Key { get; set; }
    //public string БанковскийСчетГрузополучателя_Key { get; set; }
    //public string ГруппаФинансовогоУчета_Key { get; set; }
    //public string КартаЛояльности_Key { get; set; }
    //public string Договор_Key { get; set; }
    //public string Подразделение_Key { get; set; }
    //public string Автор { get; set; }
    //public string Автор_Type { get; set; }
    //public string ПорядокРасчетов { get; set; }
    //public string Назначение_Key { get; set; }
    //public string СпособДоставки { get; set; }
    //public string ПеревозчикПартнер_Key { get; set; }
    //public string ЗонаДоставки_Key { get; set; }
    //public DateTime ВремяДоставкиС { get; set; }
    //public DateTime ВремяДоставкиПо { get; set; }
    //public string АдресДоставкиПеревозчика { get; set; }
    //public string АдресДоставкиЗначенияПолей { get; set; }
    //public string АдресДоставкиПеревозчикаЗначенияПолей { get; set; }
    //public string ДополнительнаяИнформацияПоДоставке { get; set; }
    //public string КонтактноеЛицо_Key { get; set; }
    //public string Руководитель_Key { get; set; }
    //public string ГлавныйБухгалтер_Key { get; set; }
    //public bool ВернутьМногооборотнуюТару { get; set; }
    //public int СрокВозвратаМногооборотнойТары { get; set; }
    //public string СостояниеЗаполненияМногооборотнойТары { get; set; }
    //public int СуммаВозвратнойТары { get; set; }
    //public string НазначениеПлатежа { get; set; }
    //public bool ТребуетсяЗалогЗаТару { get; set; }
    //public string Приоритет_Key { get; set; }
    //public string ИдентификаторПлатежа { get; set; }
    //public bool ОсобыеУсловияПеревозки { get; set; }
    //public string ОсобыеУсловияПеревозкиОписание { get; set; }
    //public string НаправлениеДеятельности_Key { get; set; }
    //public bool ОплатаВВалюте { get; set; }
    //public string ОбъектРасчетов_Key { get; set; }
    //public string Сборщик_Key { get; set; }
    //public string Курьер_Key { get; set; }
    //public string АдресДоставкиЗначение { get; set; }
    //public string АдресДоставкиПеревозчикаЗначение { get; set; }
    //public string ЭтапГосконтрактаЕИС { get; set; }
    //public bool ЭтоЗаказКакСчет { get; set; }
    //public DateTime ИШ_ЖелаемаяДатаДоставки { get; set; }
    //public string ИШ_ДоставкаКонтактноеЛицо { get; set; }
    //public string ИШ_ДоставкаТелефон { get; set; }
    //public string ИШ_ТипВыгрузки { get; set; }
    //public DateTime ИШ_НачалоОбеда { get; set; }
    //public DateTime ИШ_КонецОбеда { get; set; }
    //public Товары[] Товары { get; set; }
    //public Этапыграфикаоплаты[] ЭтапыГрафикаОплаты { get; set; }
    //public object[] СкидкиНаценки { get; set; }
    //public object[] НачислениеБонусныхБаллов { get; set; }
    //public object[] ДополнительныеРеквизиты { get; set; }
    //public string ПартнерnavigationLinkUrl { get; set; }
    //public string КонтрагентnavigationLinkUrl { get; set; }
    //public string ОрганизацияnavigationLinkUrl { get; set; }
    //public string СоглашениеnavigationLinkUrl { get; set; }
    //public string ВалютаnavigationLinkUrl { get; set; }
    //public string ГрафикОплатыnavigationLinkUrl { get; set; }
    //public string СкладnavigationLinkUrl { get; set; }
    //public string МенеджерnavigationLinkUrl { get; set; }
    //public string БанковскийСчетnavigationLinkUrl { get; set; }
    //public string БанковскийСчетКонтрагентаnavigationLinkUrl { get; set; }
    //public string ПодразделениеnavigationLinkUrl { get; set; }
    //public string НазначениеnavigationLinkUrl { get; set; }
    //public string ПриоритетnavigationLinkUrl { get; set; }
    //public string ОбъектРасчетовnavigationLinkUrl { get; set; }
    //public string РуководительnavigationLinkUrl { get; set; }
    //public string ГлавныйБухгалтерnavigationLinkUrl { get; set; }
    //public string ДоговорnavigationLinkUrl { get; set; }
    //public string ЗонаДоставкиnavigationLinkUrl { get; set; }
}

//public class Document_ЗаказКлиента_Товары
//{
//    public string Ref_Key { get; set; }
//    public string LineNumber { get; set; }
//    public DateTime ДатаОтгрузки { get; set; }
//    public string Номенклатура_Key { get; set; }
//    public string Характеристика_Key { get; set; }
//    public string Упаковка_Key { get; set; }
//    public float КоличествоУпаковок { get; set; }
//    public float Количество { get; set; }
//    public string ВидЦены_Key { get; set; }
//    public float Цена { get; set; }
//    public float Сумма { get; set; }
//    public string СтавкаНДС_Key { get; set; }
//    public float СуммаНДС { get; set; }
//    public float СуммаСНДС { get; set; }
//    public float ПроцентРучнойСкидки { get; set; }
//    public float СуммаРучнойСкидки { get; set; }
//    public int ПроцентАвтоматическойСкидки { get; set; }
//    public int СуммаАвтоматическойСкидки { get; set; }
//    public string ПричинаОтмены_Key { get; set; }
//    public string КодСтроки { get; set; }
//    public bool Отменено { get; set; }
//    public string КлючСвязи { get; set; }
//    public string Склад_Key { get; set; }
//    public string СрокПоставки { get; set; }
//    public string Содержание { get; set; }
//    public int СтатусУказанияСерий { get; set; }
//    public string ВариантОбеспечения { get; set; }
//    public string Серия_Key { get; set; }
//    public string НоменклатураНабора_Key { get; set; }
//    public bool Обособленно { get; set; }
//    public string ХарактеристикаНабора_Key { get; set; }
//    public string ИдентификаторСтроки { get; set; }
//    public string Подразделение_Key { get; set; }
//    public string НоменклатураПартнера_Key { get; set; }
//    public int СуммаБонусныхБалловКСписанию { get; set; }
//    public int СуммаБонусныхБалловКСписаниюВВалюте { get; set; }
//    public int СуммаНачисленныхБонусныхБалловВВалюте { get; set; }
//}

//public class Этапыграфикаоплаты
//{
//    public string Ref_Key { get; set; }
//    public string LineNumber { get; set; }
//    public string ВариантОплаты { get; set; }
//    public DateTime ДатаПлатежа { get; set; }
//    public float ПроцентПлатежа { get; set; }
//    public float СуммаПлатежа { get; set; }
//    public int ПроцентЗалогаЗаТару { get; set; }
//    public int СуммаЗалогаЗаТару { get; set; }
//    public int СуммаОтклоненияМерныхТоваров { get; set; }
//    public string Сдвиг { get; set; }
//    public string ВариантОтсчета { get; set; }
//}
