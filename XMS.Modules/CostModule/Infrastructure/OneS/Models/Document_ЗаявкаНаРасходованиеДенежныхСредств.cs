namespace XMS.Modules.CostModule.Infrastructure.OneS.Models
{
    public class Document_ЗаявкаНаРасходованиеДенежныхСредств
    {
        public string? Ref_Key { get; set; }
        public bool DeletionMark { get; set; }
        public string? Number { get; set; }
        public DateTime Date { get; set; }
        public bool Posted { get; set; }
        public string? ХозяйственнаяОперация { get; set; }
        public string? Партнер_Key { get; set; }
        public string? НазначениеПлатежа { get; set; }
        public string? Автор_Key { get; set; }
        public string? КтоЗаявил_Key { get; set; }
        public bool Закрыта { get; set; }
        public string? Организация_Key { get; set; }
        public string? Подразделение_Key { get; set; }
        public string? ДокументОснование { get; set; }
        public string? ДокументОснование_Type { get; set; }
        public string? ПриоритетОплаты_Key { get; set; }
        public string? Договор { get; set; }
        public string? Договор_Type { get; set; }
        public string? НалогообложениеНДС { get; set; }
        public string? СтатьяДвиженияДенежныхСредств_Key { get; set; }
        public decimal СуммаДокумента { get; set; }
        public string? Контрагент_Key { get; set; }
        public DateTime ЖелательнаяДатаПлатежа { get; set; }
        public string? Статус { get; set; }
        public string? Валюта_Key { get; set; }
        public List<РасшифровкаПлатежа>? РасшифровкаПлатежа { get; set; }

        public static string Uri => "Document_ЗаявкаНаРасходованиеДенежныхСредств?$format=json" +
            "&$select=Ref_Key,DeletionMark,Number,Date,Posted,ХозяйственнаяОперация,Партнер_Key,НазначениеПлатежа,Автор_Key,КтоЗаявил_Key,Закрыта," +
            "Организация_Key,Подразделение_Key,ДокументОснование,ДокументОснование_Type,ПриоритетОплаты_Key,Договор,Договор_Type,НалогообложениеНДС," +
            "СтатьяДвиженияДенежныхСредств_Key,СуммаДокумента,Контрагент_Key,ЖелательнаяДатаПлатежа,Статус,Валюта_Key,РасшифровкаПлатежа";

        public static string GetUriByRefKey(string refKey)
        {
            string uri = $"{Uri}&$filter=Ref_Key eq guid'refKey'";

            return uri;
        }

        public static string GetUriByDate(DateTime? begin = null, DateTime? end = null)
        {
            string uri = $"{Uri}&$filter=DeletionMark eq false and Posted eq true and Date ge datetime'{begin:s}' and Date lt datetime'{end:s}'";

            return uri;
        }

        //public string? DataVersion { get; set; }
        //public bool ФормаОплатыНаличная { get; set; }
        //public bool ФормаОплатыБезналичная { get; set; }
        //public bool ФормаОплатыПлатежнаяКарта { get; set; }
        //public string? БанковскийСчет_Key { get; set; }
        //public string? Касса_Key { get; set; }
        //public string? БанковскийСчетКонтрагента_Key { get; set; }
        //public string? ПодотчетноеЛицо_Key { get; set; }
        //public string? ОрганизацияПолучатель_Key { get; set; }
        //public string? БанковскийСчетПолучатель_Key { get; set; }
        //public string? КассаПолучатель_Key { get; set; }
        //public string? КтоРешил_Key { get; set; }
        //public string? Комментарий { get; set; }
        //public string? ФормаОплатыЗаявки { get; set; }
        //public DateTime ДатаПлатежа { get; set; }
        //public string? ВалютаКонвертации_Key { get; set; }
        //public int СуммаКонвертации { get; set; }
        //public int КурсКонвертации { get; set; }
        //public string? ПланированиеСуммы { get; set; }
        //public string? НомерВедомостиНаВыплатуЗарплаты { get; set; }
        //public DateTime ДатаВедомостиНаВыплатуЗарплаты { get; set; }
        //public bool СверхЛимита { get; set; }
        //public string? СтатьяАктивовПассивов_Key { get; set; }
        //public string? АналитикаАктивовПассивов { get; set; }
        //public string? АналитикаАктивовПассивов_Type { get; set; }
        //public string? ИдентификаторПлатежа { get; set; }
        //public string? ХозяйственнаяОперацияПоЗарплате { get; set; }
        //public DateTime ДатаАвансовогоОтчета { get; set; }
        //public bool ДоговорСУчастникомГОЗ { get; set; }
        //public bool ПлатежиПо275ФЗ { get; set; }
        //public string? ТипПлатежаФЗ275 { get; set; }
        //public string? ПредметОплаты { get; set; }
        //public string? ВариантОплаты { get; set; }
        //public string? ПунктКонтрактаПредмета { get; set; }
        //public string? ПунктКонтрактаОплаты { get; set; }
        //public bool ПеречислениеВБюджет { get; set; }
        //public string? ВидПеречисленияВБюджет { get; set; }
        //public string? КодОКАТО { get; set; }
        //public string? ПоказательОснования { get; set; }
        //public string? ПоказательПериода { get; set; }
        //public string? ПоказательНомера { get; set; }
        //public string? ПоказательДаты { get; set; }
        //public string? ПоказательТипа { get; set; }
        //public string? СтатусСоставителя { get; set; }
        //public string? КодБК { get; set; }
        //public string? ТипНалога_Key { get; set; }
        //public string? УдалитьТипНалога { get; set; }
        //public bool НДФЛПоВедомостям { get; set; }
        //public string? РегистрацияВНалоговомОргане_Key { get; set; }
        //public string? ИННПлательщика { get; set; }
        //public string? КПППлательщика { get; set; }
        //public string? ТекстПлательщика { get; set; }
        //public string? ТипКомиссииЗаПеревод { get; set; }
        //public string? ИнформацияПолучателюПлатежа { get; set; }
        //public string? УсловиеСделкиКонвертации { get; set; }
        //public string? БанковскийСчетСписанияКомиссии_Key { get; set; }
        //public string? ГруппаФинансовогоУчета_Key { get; set; }
        //public string? НаправлениеДеятельности_Key { get; set; }
        //public int КратностьКурсаКонвертации { get; set; }
        //public bool СписокФизЛиц { get; set; }
        //public string? КодВидаДохода { get; set; }
        //public string? ДоговорЭквайринга_Key { get; set; }
        //public bool ОтражатьКомиссию { get; set; }
        //public int СуммаКомиссии { get; set; }
        //public string? СтатьяРасходов_Key { get; set; }
        //public string? АналитикаРасходов { get; set; }
        //public string? АналитикаРасходов_Type { get; set; }
        //public string? НастройкаСчетовУчета { get; set; }
        //public string? КодВыплат { get; set; }
        //public bool КонтролироватьОплатуПоОбъектамРасчетов { get; set; }
        //public bool ПеречислениеСотрудникуЧерезБанк { get; set; }
        //public string? СтатьяЦелевыхСредств { get; set; }
        //public bool ПлатежСКонвертацией { get; set; }
        //public bool СписокКонтрагентов { get; set; }
        //public bool ОперацияССамозанятым { get; set; }
        //public object[] ЛицевыеСчетаСотрудников { get; set; }
        //public object[] ДоговорыСЗаказчиками { get; set; }
        //public object[] ПодтверждающиеДокументы { get; set; }
        //public object[] РаспределениеПоСчетам { get; set; }
        //public object[] ИнструкцииБанку { get; set; }
        //public object[] ДополнительныеРеквизиты { get; set; }
        //public object[] БанковскиеСчетаСпискаКонтрагентов { get; set; }
        //public string? ОрганизацияnavigationLinkUrl { get; set; }
        //public string? ВалютаnavigationLinkUrl { get; set; }
        //public string? ПодразделениеnavigationLinkUrl { get; set; }
        //public string? КтоЗаявилnavigationLinkUrl { get; set; }
        //public string? АвторnavigationLinkUrl { get; set; }
        //public string? ПриоритетОплатыnavigationLinkUrl { get; set; }
        //public string? ПодотчетноеЛицоnavigationLinkUrl { get; set; }
        //public string? КтоРешилnavigationLinkUrl { get; set; }
        //public string? СтатьяДвиженияДенежныхСредствnavigationLinkUrl { get; set; }
        //public string? КонтрагентnavigationLinkUrl { get; set; }
        //public string? ПартнерnavigationLinkUrl { get; set; }
        //public string? БанковскийСчетКонтрагентаnavigationLinkUrl { get; set; }
    }

    public class РасшифровкаПлатежа
    {
        public string? Ref_Key { get; set; }
        public string? LineNumber { get; set; }
        public string? Партнер_Key { get; set; }
        public float Сумма { get; set; }
        public string? ВалютаВзаиморасчетов_Key { get; set; }
        public float СуммаВзаиморасчетов { get; set; }
        public string? СтатьяРасходов { get; set; }
        public string? СтатьяРасходов_Type { get; set; }
        public string? СтатьяДвиженияДенежныхСредств_Key { get; set; }
        public string? Комментарий { get; set; }
        public string? Подразделение_Key { get; set; }
        public string? НаправлениеДеятельности_Key { get; set; }
        public string? СтавкаНДС_Key { get; set; }
        public string? Организация_Key { get; set; }
        public string? ОбъектРасчетов_Key { get; set; }

        //public string? АналитикаРасходов { get; set; }
        //public string? АналитикаРасходов_Type { get; set; }
        //public string? ДоговорКредитаДепозита_Key { get; set; }
        //public string? ДоговорАренды { get; set; }
        //public string? ТипПлатежаПоАренде { get; set; }
        //public string? АналитикаАктивовПассивов { get; set; }
        //public string? АналитикаАктивовПассивов_Type { get; set; }
        //public string? ДоговорЗаймаСотруднику { get; set; }
        //public string? ТипСуммыКредитаДепозита { get; set; }
        //public float СуммаНДС { get; set; }
        //public string? Ведомость { get; set; }
        //public string? НастройкаСчетовУчета { get; set; }
        //public int КурсЧислительВзаиморасчетов { get; set; }
        //public int КурсЗнаменательВзаиморасчетов { get; set; }
        //public string? СтатьяЦелевыхСредств { get; set; }
        //public DateTime ДатаПогашения { get; set; }
        //public string? Контрагент_Key { get; set; }
    }
}
