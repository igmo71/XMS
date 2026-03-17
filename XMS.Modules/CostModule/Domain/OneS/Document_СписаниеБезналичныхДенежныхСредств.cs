using System.ComponentModel.DataAnnotations;
using XMS.Application;
using XMS.Application.Abstractions.Integration;

namespace XMS.Modules.CostModule.Domain.OneS
{
    public class Document_СписаниеБезналичныхДенежныхСредств : IOneSDocument
    {
        [MaxLength(AppSettings.OneS.GUID)] public required string Ref_Key { get; set; }
        [MaxLength(AppSettings.OneS.VALUE)] public string? Number { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(AppSettings.OneS.VALUE)] public string? ХозяйственнаяОперация { get; set; }
        [MaxLength(AppSettings.OneS.GUID)] public string? Партнер_Key { get; set; }
        [MaxLength(AppSettings.OneS.COMMENT)] public string? НазначениеПлатежа { get; set; }
        [MaxLength(AppSettings.OneS.GUID)] public string? Автор_Key { get; set; }
        [MaxLength(AppSettings.OneS.GUID)] public string? Организация_Key { get; set; }
        [MaxLength(AppSettings.OneS.GUID)] public string? Подразделение_Key { get; set; }
        [MaxLength(AppSettings.OneS.GUID)] public string? ДокументОснование { get; set; }
        [MaxLength(AppSettings.OneS.VALUE)] public string? ДокументОснование_Type { get; set; }
        [MaxLength(AppSettings.OneS.GUID)] public string? Договор { get; set; }
        [MaxLength(AppSettings.OneS.VALUE)] public string? Договор_Type { get; set; }
        [MaxLength(AppSettings.OneS.VALUE)] public string? НалогообложениеНДС { get; set; }
        [MaxLength(AppSettings.OneS.GUID)] public string? СтатьяДвиженияДенежныхСредств_Key { get; set; }
        [MaxLength(AppSettings.OneS.GUID)] public string? Контрагент_Key { get; set; }
        [MaxLength(AppSettings.OneS.GUID)] public string? Валюта_Key { get; set; }
        public decimal СуммаДокумента { get; set; }
        [MaxLength(AppSettings.OneS.COMMENT)] public string? Комментарий { get; set; }
        public List<Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа>? РасшифровкаПлатежа { get; set; }

        public static string Uri => "Document_СписаниеБезналичныхДенежныхСредств?$format=json" +
           "&$select=Ref_Key,DeletionMark,Number,Date,Posted,ХозяйственнаяОперация,Партнер_Key,НазначениеПлатежа,Автор_Key,Организация_Key,Подразделение_Key," +
            "ДокументОснование,ДокументОснование_Type,Договор,Договор_Type,НалогообложениеНДС,СтатьяДвиженияДенежныхСредств_Key,СуммаДокумента,Контрагент_Key," +
            "Валюта_Key,РасшифровкаПлатежа";

        public static string GetUriByRefKey(string refKey)
        {
            string uri = $"{Uri}&$filter=Ref_Key eq guid'{refKey}'";

            return uri;
        }

        public static string GetUriByDate(DateTime? from = null, DateTime? to = null)
        {
            string uri = $"{Uri}&$filter=DeletionMark eq false and Posted eq true and Date ge datetime'{from:s}' and Date lt datetime'{to:s}'";

            return uri;
        }

        //public bool DeletionMark { get; set; }
        //public bool Posted { get; set; }

        //public string? DataVersion { get; set; }
        //public DateTime ДатаВходящегоДокумента { get; set; }
        //public string? НомерВходящегоДокумента { get; set; }
        //public int ОчередностьПлатежа { get; set; }
        //public string? БанковскийСчетПолучатель_Key { get; set; }
        //public string? КассаПолучатель_Key { get; set; }
        //public string? ПодотчетноеЛицо_Key { get; set; }
        //public string? ЗаявкаНаРасходованиеДенежныхСредств { get; set; }
        //public string? ЗаявкаНаРасходованиеДенежныхСредств_Type { get; set; }
        //public string? РаспоряжениеНаПеремещениеДенежныхСредств_Key { get; set; }
        //public string? БанковскийСчетКонтрагента_Key { get; set; }
        //public string? БанковскийСчет_Key { get; set; }
        //public bool ПеречислениеВБюджет { get; set; }
        //public string? ВидПеречисленияВБюджет { get; set; }
        //public string? СтатусСоставителя { get; set; }
        //public string? ПоказательТипа { get; set; }
        //public string? КодБК { get; set; }
        //public string? КодОКАТО { get; set; }
        //public string? ПоказательОснования { get; set; }
        //public string? ПоказательПериода { get; set; }
        //public string? ПоказательНомера { get; set; }
        //public string? ПоказательДаты { get; set; }
        
        //public bool НеКонтролироватьЗаполнениеЗаявки { get; set; }
        //public string? ВидПлатежа { get; set; }
        //public string? ТипПлатежногоДокумента { get; set; }
        //public string? ВалютаКонвертации_Key { get; set; }
        //public int КурсКонвертации { get; set; }
        //public int СуммаКонвертации { get; set; }
        //public string? НомерВедомостиНаВыплатуЗарплаты { get; set; }
        //public DateTime ДатаВедомостиНаВыплатуЗарплаты { get; set; }
        //public bool ОплатаПоЗаявкам { get; set; }
        //public string? ИдентификаторПлатежа { get; set; }
        //public bool ПроводкиПоРаботникам { get; set; }
        //public bool ПроведеноБанком { get; set; }
        //public DateTime ДатаПроведенияБанком { get; set; }
        //public string? РегистрацияВНалоговомОргане_Key { get; set; }
        //public DateTime ДатаВыгрузки { get; set; }
        //public DateTime ДатаЗагрузки { get; set; }
        //public string? ОшибкиЗагрузки { get; set; }
        //public string? ФорматированноеНазначениеПлатежа { get; set; }
        //public string? ДанныеВыписки { get; set; }
        //public string? ИмяКонтрагента { get; set; }
        //public DateTime ДатаАвансовогоОтчета { get; set; }
        //public bool ПлатежиПо275ФЗ { get; set; }
        //public string? ТипПлатежаФЗ275 { get; set; }
        //public bool ДоговорСУчастникомГОЗ { get; set; }
        //public string? ДоговорСЗаказчиком { get; set; }
        //public string? ДоговорСЗаказчиком_Type { get; set; }
        //public string? ТипНалога_Key { get; set; }
        //public string? УдалитьТипНалога { get; set; }
        //public bool НДФЛПоВедомостям { get; set; }
        //public string? ГруппаФинансовогоУчета_Key { get; set; }
        //public string? НаправлениеДеятельности_Key { get; set; }
        //public string? ИННПлательщика { get; set; }
        //public string? КПППлательщика { get; set; }
        //public string? ТекстПлательщика { get; set; }
        //public string? Ответственный_Key { get; set; }
        //public string? ТипКомиссииЗаПеревод { get; set; }
        //public string? КодВалютнойОперации_Key { get; set; }
        //public string? ИнформацияДляВалютногоКонтроля { get; set; }
        //public string? ИнформацияПолучателюПлатежа { get; set; }
        //public string? ИнформацияДляРегулирующихОрганов { get; set; }
        //public string? УсловиеСделкиКонвертации { get; set; }
        //public string? БанковскийСчетСписанияКомиссии_Key { get; set; }
        //public string? УведомлениеОЗачисленииВалюты_Key { get; set; }
        //public int КратностьКурсаКонвертации { get; set; }
        //public string? ОбъектРасчетов_Key { get; set; }
        //public string? ИдентификаторДокумента { get; set; }
        //public string? КодВидаДохода { get; set; }
        //public string? ДоговорЭквайринга_Key { get; set; }
        //public bool ОтражатьКомиссию { get; set; }
        //public int СуммаКомиссии { get; set; }
        //public string? СтатьяРасходов_Key { get; set; }
        //public string? АналитикаРасходов { get; set; }
        //public string? АналитикаРасходов_Type { get; set; }
        //public string? НастройкаСчетовУчета { get; set; }
        //public string? КодВыплат { get; set; }
        //public bool Исправление { get; set; }
        //public string? СторнируемыйДокумент_Key { get; set; }
        //public string? ИсправляемыйДокумент_Key { get; set; }
        //public bool ПеречислениеСотрудникуЧерезБанк { get; set; }
        //public DateTime ПериодРегистрации { get; set; }
        //public bool СписокФизЛиц { get; set; }
        //public DateTime ДатаВыгрузкиРеестра { get; set; }
        //public string? НомерДоговораСБанком { get; set; }
        //public DateTime ДатаДоговораСБанком { get; set; }
        //public string? ОтделениеБанка { get; set; }
        //public string? ФилиалОтделенияБанка { get; set; }
        //public string? СтатьяЦелевыхСредств { get; set; }
        //public bool ПлатежСКонвертацией { get; set; }
        //public int СуммаВВалютеОтправителя { get; set; }
        //public string? СтатьяДвиженияДенежныхСредствКонвертация_Key { get; set; }
        //public bool СписокКонтрагентов { get; set; }
        //public bool ОперацияССамозанятым { get; set; }
        //public string? КодировкаФайла { get; set; }
        //public string? ВидЗачисления { get; set; }
        //public object[] ЛицевыеСчетаСотрудников { get; set; }
        //public object[] ИнструкцииБанку { get; set; }
        //public object[] ДополнительныеРеквизиты { get; set; }
        //public object[] БанковскиеСчетаСпискаКонтрагентов { get; set; }
        //public string? КонтрагентnavigationLinkUrl { get; set; }
        //public string? ОрганизацияnavigationLinkUrl { get; set; }
        //public string? ВалютаnavigationLinkUrl { get; set; }
        //public string? БанковскийСчетКонтрагентаnavigationLinkUrl { get; set; }
        //public string? БанковскийСчетnavigationLinkUrl { get; set; }
        //public string? ОтветственныйnavigationLinkUrl { get; set; }
        //public string? ПартнерnavigationLinkUrl { get; set; }
        //public string? АвторnavigationLinkUrl { get; set; }
        //public string? СтатьяДвиженияДенежныхСредствnavigationLinkUrl { get; set; }
        //public string? БанковскийСчетПолучательnavigationLinkUrl { get; set; }
        //public string? ОбъектРасчетовnavigationLinkUrl { get; set; }
        //public string? ТипНалогаnavigationLinkUrl { get; set; }
    }

    public class Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа : IOneSDocumentItem
    {
        [MaxLength(AppSettings.OneS.GUID)] public required string Ref_Key { get; set; }
        public int LineNumber { get; set; }
        [MaxLength(AppSettings.OneS.GUID)] public string? Партнер_Key { get; set; }
        [MaxLength(AppSettings.OneS.GUID)] public string? СтатьяРасходов { get; set; }
        [MaxLength(AppSettings.OneS.VALUE)] public string? СтатьяРасходов_Type { get; set; }
        [MaxLength(AppSettings.OneS.GUID)] public string? СтатьяДвиженияДенежныхСредств_Key { get; set; }
        [MaxLength(AppSettings.OneS.COMMENT)] public string? Комментарий { get; set; }
        [MaxLength(AppSettings.OneS.GUID)] public string? Подразделение_Key { get; set; }
        [MaxLength(AppSettings.OneS.GUID)] public string? НаправлениеДеятельности_Key { get; set; }
        [MaxLength(AppSettings.OneS.GUID)] public string? СтавкаНДС_Key { get; set; }
        [MaxLength(AppSettings.OneS.GUID)] public string? ОбъектРасчетов_Key { get; set; }
        public decimal Сумма { get; set; }
        [MaxLength(AppSettings.OneS.GUID)] public string? ВалютаВзаиморасчетов_Key { get; set; }
        public decimal СуммаВзаиморасчетов { get; set; }

        //public string? АналитикаРасходов { get; set; }
        //public string? АналитикаРасходов_Type { get; set; }
        //public string? ДоговорКредитаДепозита_Key { get; set; }
        //public string? ЗаявкаНаРасходованиеДенежныхСредств { get; set; }
        //public string? ЗаявкаНаРасходованиеДенежныхСредств_Type { get; set; }
        //public string? ДоговорАренды { get; set; }
        //public string? ТипПлатежаПоАренде { get; set; }
        //public string? ИдентификаторСтроки { get; set; }
        //public string? АналитикаАктивовПассивов { get; set; }
        //public string? АналитикаАктивовПассивов_Type { get; set; }
        //public string? ДоговорЗаймаСотруднику { get; set; }
        //public string? ТипСуммыКредитаДепозита { get; set; }
        //public string? ДоговорСЗаказчиком_Key { get; set; }
        //public string? СтатьяКалькуляции { get; set; }
        //public float СуммаНДС { get; set; }
        //public string? Ведомость { get; set; }
        //public string? НастройкаСчетовУчета { get; set; }
        //public int КурсЧислительВзаиморасчетов { get; set; }
        //public int КурсЗнаменательВзаиморасчетов { get; set; }
        //public string? СтатьяЦелевыхСредств { get; set; }
        //public DateTime ДатаПогашения { get; set; }
        //public string? Контрагент_Key { get; set; }
        //public int СуммаВВалютеОтправителя { get; set; }
        //public int СуммаНДСВВалютеОтправителя { get; set; }
    }

}
