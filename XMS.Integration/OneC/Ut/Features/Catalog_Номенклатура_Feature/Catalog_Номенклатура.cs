using Microsoft.Extensions.Hosting;
using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Номенклатура_Feature;

public class Catalog_Номенклатура : ICatalog
{
    public Guid Ref_Key { get; set; }
    public string? DataVersion { get; set; }
    public bool DeletionMark { get; set; }
    public Guid Parent_Key { get; set; }
    public bool IsFolder { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }

    public static string Uri => "Catalog_Номенклатура?$format=json&$inlinecount=allpages" +
        "&$select=Ref_Key,DataVersion,DeletionMark,Parent_Key,IsFolder,Code,Description";

    public static string GetUriByRefKey(Guid refKey) => $"{Uri}&$filter=Ref_Key eq guid'{refKey}'";

    public static string GetExchangeName(IHostEnvironment hostEnvironment) =>
        hostEnvironment.IsDevelopment() ? $"dev_{nameof(Catalog_Номенклатура)}" : $"{nameof(Catalog_Номенклатура)}";

    public static string GetQueueName(IHostEnvironment hostEnvironment) =>
        hostEnvironment.IsDevelopment() ? $"dev_{nameof(Catalog_Номенклатура)}" : $"xms_{nameof(Catalog_Номенклатура)}";

    //public string Артикул { get; set; }
    //public bool АлкогольнаяПродукция { get; set; }
    //public string ВариантОформленияПродажи { get; set; }
    //public string ВесЕдиницаИзмерения_Key { get; set; }
    //public int? ВесЗнаменатель { get; set; }
    //public bool? ВесИспользовать { get; set; }
    //public bool? ВесМожноУказыватьВДокументах { get; set; }
    //public float? ВесЧислитель { get; set; }
    //public bool? ВестиУчетПоГТД { get; set; }
    //public bool? ВестиУчетСертификатовНоменклатуры { get; set; }
    //public string ВидАлкогольнойПродукции_Key { get; set; }
    //public string ВидНоменклатуры_Key { get; set; }
    //public string ГруппаДоступа_Key { get; set; }
    //public string ГруппаФинансовогоУчета_Key { get; set; }
    //public string ЕдиницаИзмерения_Key { get; set; }
    //public string ЕдиницаИзмеренияСрокаГодности { get; set; }
    //public bool? ЕстьТоварыДругогоКачества { get; set; }
    //public bool? ИмпортнаяАлкогольнаяПродукция { get; set; }
    //public string ДлинаЕдиницаИзмерения_Key { get; set; }
    //public int? ДлинаЗнаменатель { get; set; }
    //public bool? ДлинаИспользовать { get; set; }
    //public bool? ДлинаМожноУказыватьВДокументах { get; set; }
    //public int? ДлинаЧислитель { get; set; }
    //public string ИспользованиеХарактеристик { get; set; }
    //public bool? ИспользоватьИндивидуальныйШаблонЦенника { get; set; }
    //public bool? ИспользоватьИндивидуальныйШаблонЭтикетки { get; set; }
    //public bool? ИспользоватьУпаковки { get; set; }
    //public string Качество { get; set; }
    //public string КодДляПоиска { get; set; }
    //public string Марка_Key { get; set; }
    //public string НаборУпаковок_Key { get; set; }
    //public string НаименованиеПолное { get; set; }
    //public string НоменклатураМногооборотнаяТара_Key { get; set; }
    //public int? ОбъемДАЛ { get; set; }
    //public string Описание { get; set; }
    //public bool? ПодакцизныйТовар { get; set; }
    //public bool? ПоставляетсяВМногооборотнойТаре { get; set; }
    //public string Производитель_Key { get; set; }
    //public string ПроизводительИмпортерДляДекларацийАлко_Key { get; set; }
    //public string СкладскаяГруппа_Key { get; set; }
    //public string СрокГодности { get; set; }
    //public string СтавкаНДС { get; set; }
    //public string ТипНоменклатуры { get; set; }
    //public string ТоварнаяКатегория_Key { get; set; }
    //public string ФайлКартинки_Key { get; set; }
    //public string ФайлОписанияДляСайта_Key { get; set; }
    //public string ОбъемЕдиницаИзмерения_Key { get; set; }
    //public int? ОбъемЗнаменатель { get; set; }
    //public bool? ОбъемИспользовать { get; set; }
    //public bool? ОбъемМожноУказыватьВДокументах { get; set; }
    //public int? ОбъемЧислитель { get; set; }
    //public string ХарактеристикаМногооборотнаяТара_Key { get; set; }
    //public string ПлощадьЕдиницаИзмерения_Key { get; set; }
    //public int? ПлощадьЗнаменатель { get; set; }
    //public string СхемаОбеспечения_Key { get; set; }
    //public string СпособОбеспеченияПотребностей_Key { get; set; }
    //public bool? ПлощадьИспользовать { get; set; }
    //public bool? ПлощадьМожноУказыватьВДокументах { get; set; }
    //public int? ПлощадьЧислитель { get; set; }
    //public string ЦеноваяГруппа_Key { get; set; }
    //public string ШаблонЦенника_Key { get; set; }
    //public string ЕдиницаДляОтчетов_Key { get; set; }
    //public int? КоэффициентЕдиницыДляОтчетов { get; set; }
    //public string ШаблонЭтикетки_Key { get; set; }
    //public string СезоннаяГруппа_Key { get; set; }
    //public string КоллекцияНоменклатуры_Key { get; set; }
    //public string Принципал { get; set; }
    //public string Принципал_Type { get; set; }
    //public string Контрагент { get; set; }
    //public string Контрагент_Type { get; set; }
    //public string РейтингПродаж_Key { get; set; }
    //public bool? ОбособленнаяЗакупкаПродажа { get; set; }
    //public string ГруппаАналитическогоУчета_Key { get; set; }
    //public string КодТНВЭД_Key { get; set; }
    //public string КодОКВЭД_Key { get; set; }
    //public string КодОКП_Key { get; set; }
    //public bool? ОблагаетсяНДПИПоПроцентнойСтавке { get; set; }
    //public string ВладелецСерий_Key { get; set; }
    //public string ВладелецХарактеристик_Key { get; set; }
    //public string ВладелецТоварныхКатегорий_Key { get; set; }
    //public int? Крепость { get; set; }
    //public string ОсобенностьУчета { get; set; }
    //public bool? ПродукцияМаркируемаяДляГИСМ { get; set; }
    //public bool? КиЗГИСМ { get; set; }
    //public string КиЗГИСМВид { get; set; }
    //public string КиЗГИСМСпособВыпускаВОборот { get; set; }
    //public string КиЗГИСМGTIN { get; set; }
    //public string КиЗГИСМРазмер { get; set; }
    //public bool? УдалитьСырьевойТовар { get; set; }
    //public bool? ПодконтрольнаяПродукцияВЕТИС { get; set; }
    //public bool? АлкогольнаяПродукцияВоВскрытойТаре { get; set; }
    //public string ГоловнаяНоменклатура { get; set; }
    //public string ГоловнаяНоменклатура_Type { get; set; }
    //public int? КоэффициентГоловной { get; set; }
    //public string КодРаздел7ДекларацииНДС { get; set; }
    //public bool? ОблагаетсяНДСУПокупателя { get; set; }
    //public string КодОКВЭД2_Key { get; set; }
    //public string КодОКПД2_Key { get; set; }
    //public DateTime? Доброга_ДатаСоздания { get; set; }
    //public string Б_Идентификатор { get; set; }
    //public string Б_НомерВерсии { get; set; }
    //public Дополнительныереквизиты[] ДополнительныеРеквизиты { get; set; }
    //public object[] ДрагоценныеМатериалы { get; set; }
    //public bool Predefined { get; set; }
    //public string PredefinedDataName { get; set; }
}

//public class Дополнительныереквизиты
//{
//    public string Ref_Key { get; set; }
//    public string LineNumber { get; set; }
//    public string Свойство_Key { get; set; }
//    public string Значение { get; set; }
//    public string Значение_Type { get; set; }
//    public string ТекстоваяСтрока { get; set; }
//}

