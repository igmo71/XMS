namespace XMS.Modules.GodooModule.Domain
{
    public class Catalog_Номенклатура
    {
        public string? Ref_Key { get; set; }
        public string? Description { get; set; }
        public string? НаименованиеПолное { get; set; }
        public string? Code { get; set; }
        public string? Артикул { get; set; }

        /// <summary>
        /// Справочник.Виды Номенклатуры
        /// Товары
        /// </summary>
        public string? ВидНоменклатуры_Key { get; set; } = "9804c0e0-9a9d-11e6-958f-000c29aa418a";

        /// <summary>
        /// Справочник.КлассификаторЕдиницИзмерения
        /// шт
        /// </summary>
        public string? ЕдиницаИзмерения_Key { get; set; } = "ad5336cb-7390-11e7-80e3-0cc47adeb013";

        public static string Uri => "Catalog_Номенклатура?$format=json&$inlinecount=allpages&$select=Ref_Key,Description,Артикул";

        //public bool IsFolder { get; set; }
        //public string DataVersion { get; set; }
        //public bool DeletionMark { get; set; }
        //public string Parent_Key { get; set; }
        //public string Комментарий { get; set; }
        //public bool? Услуга { get; set; }
        //public string НоменклатурнаяГруппа_Key { get; set; }
        //public string СтранаПроисхождения_Key { get; set; }
        //public string НомерГТД_Key { get; set; }
        //public string СтатьяЗатрат_Key { get; set; }
        //public string ОсновнаяСпецификацияНоменклатуры_Key { get; set; }
        //public string Производитель_Key { get; set; }
        //public string Импортер_Key { get; set; }
        //public string КодТНВЭД_Key { get; set; }
        //public string КодОКВЭД2_Key { get; set; }
        //public string КодОКВЭД_Key { get; set; }
        //public string КодОКП_Key { get; set; }
        //public string КодОКПД2_Key { get; set; }
        //public string УдалитьСтавкаНДС { get; set; }
        //public bool? ПродукцияМаркируемаяДляГИСМ { get; set; }
        //public string ПериодичностьУслуги { get; set; }
        //public string КодРаздел7ДекларацииНДС_Key { get; set; }
        //public string Операции0_Key { get; set; }
        //public bool? ПодконтрольнаяПродукцияВЕТИС { get; set; }
        //public string ВидСтавкиНДС { get; set; }
        //public bool? ТабачнаяПродукция { get; set; }
        //public bool? ОбувнаяПродукция { get; set; }
        //public bool? ЛегкаяПромышленность { get; set; }
        //public bool? МолочнаяПродукцияПодконтрольнаяВЕТИС { get; set; }
        //public bool? Шины { get; set; }
        //public bool? Духи { get; set; }
        //public bool? Велосипеды { get; set; }
        //public bool? КреслаКоляски { get; set; }
        //public bool? Фотоаппараты { get; set; }
        //public bool? СредствоИндивидуальнойЗащиты { get; set; }
        //public string КодНоменклатурнойКлассификацииККТ_Key { get; set; }
        //public bool? ПрослеживаемыйТовар { get; set; }
        //public int? ВесПоСертификатуТовара { get; set; }
        //public bool? АльтернативныйТабак { get; set; }
        //public bool? УпакованнаяВода { get; set; }
        //public bool? МолочнаяПродукцияБезВЕТИС { get; set; }
        //public string Описание { get; set; }
        //public string КодВидаТРУ { get; set; }
        //public bool? Антисептики { get; set; }
        //public bool? БАДы { get; set; }
        //public bool? НикотиносодержащаяПродукция { get; set; }
        //public bool? Пиво { get; set; }
        //public bool? ПрослеживаемыйКомплект { get; set; }
        //public bool? БезалкогольноеПиво { get; set; }
        //public bool? СоковаяПродукция { get; set; }
        //public bool? Зерно { get; set; }
        //public bool? ПродуктыПереработкиЗерна { get; set; }
        //public string ПризнакПредметаРасчета { get; set; }
        //public bool? МорепродуктыПодконтрольныеВЕТИС { get; set; }
        //public bool? ЗерноВЕТИС { get; set; }
        //public bool? ПродуктыПереработкиЗернаВЕТИС { get; set; }
        //public bool? ПодконтрольнаяПродукцияСАТУРН { get; set; }
        //public string КодВидаПодакцизногоТовара_Key { get; set; }
        //public int? КоэффициентПересчета { get; set; }
        //public string ЕдиницаИзмеренияДляРасчетаАкциза_Key { get; set; }
        //public bool? КормаДляЖивотныхПодконтрольныеВЕТИС { get; set; }
        //public bool? МясоПодконтрольноеВЕТИС { get; set; }
        //public bool? ВетеринарныеПрепараты { get; set; }
        //public bool? ИгрыИИгрушкиДляДетей { get; set; }
        //public bool? РадиоэлектроннаяПродукция { get; set; }
        //public bool? ТитановаяМеталлопродукция { get; set; }
        //public bool? КонсервированнаяПродукцияПодконтрольнаяВЕТИС { get; set; }
        //public bool? РастительныеМасла { get; set; }
        //public bool? ОптоволокноИОптоволоконнаяПродукция { get; set; }
        //public bool? ПарфюмерныеИКосметическиеСредстваИБытоваяХимия { get; set; }
        //public bool? КормаДляЖивотныхБезВЕТИС { get; set; }
        //public bool? КонсервированнаяПродукцияБезВЕТИС { get; set; }
        //public string УслугаРазмещения { get; set; }
        //public bool? ПечатнаяПродукция { get; set; }
        //public bool? СтроительныеМатериалы { get; set; }
        //public bool? ОтопительныеПриборы { get; set; }
        //public bool? Бакалея { get; set; }
        //public bool? АлкогольнаяПродукцияДо9Процентов { get; set; }
        //public bool? ТелефоныИНоутбуки { get; set; }
        //public bool? ПиротехническиеИзделияИСредстваПожарнойБезопасности { get; set; }
        //public bool? КабельнаяПродукция { get; set; }
        //public bool? МоторныеМасла { get; set; }
        //public bool? БезалкогольныеНапитки { get; set; }
        //public bool? ПивоВПотребительскихУпаковках { get; set; }
        //public bool? ТехническиеСредстваРеабилитации { get; set; }
        //public bool? МедицинскиеИзделия { get; set; }
        //public bool ВАрхиве { get; set; }
        //public bool? МедицинскиеИзделия20 { get; set; }
        //public bool? КормаДляЖивотныхВлажныеПодконтрольныеВЕТИС { get; set; }
        //public bool? КормаДляЖивотныхВлажныеБезВЕТИС { get; set; }
        //public bool? ЛегкаяПромышленность2025 { get; set; }
        //public string ВидМаркировки { get; set; }
        //public object[] ДополнительныеРеквизиты { get; set; }
        //public object[] ИсторияВидаСтавкиНДС { get; set; }
        //public object[] ИсторияПрослеживаемогоТовара { get; set; }
        //public bool Predefined { get; set; }
        //public string PredefinedDataName { get; set; }
        //public string ParentnavigationLinkUrl { get; set; }
        //public string ВидНоменклатурыnavigationLinkUrl { get; set; }
        //public string ЕдиницаИзмеренияnavigationLinkUrl { get; set; }
        //public string НоменклатурнаяГруппаnavigationLinkUrl { get; set; }
        //public string СтранаПроисхожденияnavigationLinkUrl { get; set; }
    }

}
