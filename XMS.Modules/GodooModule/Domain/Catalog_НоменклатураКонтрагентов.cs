namespace XMS.Modules.GodooModule.Domain
{
    public class Catalog_НоменклатураКонтрагентов
    {
        public string? Ref_Key { get; set; }
        public string? Description { get; set; }
        public string? НаименованиеНоменклатуры { get; set; }
        public string? Code { get; set; }
        public string? Артикул { get; set; }
        public string? ВладелецНоменклатуры_Key { get; set; }

        internal static string GetUri(string yunuProductId)
        {
            string uri = $"Catalog_НоменклатураКонтрагентов?$format=json&$inlinecount=allpages" +
                $"&$select=Ref_Key,Description,Артикул" +
                $"&$filter=Артикул eq '{yunuProductId}' and DeletionMark eq false";
            return uri;
        }

        //public string DataVersion { get; set; }
        //public bool DeletionMark { get; set; }
        //public string Parent_Key { get; set; }
        //public bool IsFolder { get; set; }        
        //public string Номенклатура { get; set; }
        //public string Номенклатура_Type { get; set; }
        //public string Характеристика_Key { get; set; }
        //public string Упаковка_Key { get; set; }
        //public string Идентификатор { get; set; }
        //public string ИдентификаторНоменклатуры { get; set; }
        //public string ИдентификаторХарактеристики { get; set; }
        //public string ИдентификаторУпаковки { get; set; }
        //public string ВладелецНоменклатуры_Key { get; set; }
        //public string НаименованиеХарактеристики { get; set; }
        //public string НаименованиеУпаковки { get; set; }
        //public string НаименованиеБазовойЕдиницыИзмерения { get; set; }
        //public string КодОКЕИБазовойЕдиницыИзмерения { get; set; }
        //public int КоличествоБазовойЕдиницыИзмерения { get; set; }
        //public int КоличествоУпаковок { get; set; }
        //public string ИдентификаторНоменклатурыСервиса { get; set; }
        //public string ИдентификаторХарактеристикиСервиса { get; set; }
        //public bool ИспользоватьХарактеристики { get; set; }
        //public string СтавкаНДС { get; set; }
        //public bool Недействителен { get; set; }
        //public string Штрихкод { get; set; }
        //public string ДругиеШтрихкодыНоменклатурыСтрокой { get; set; }
        //public string ТипНоменклатурыФНС { get; set; }
        //public string КодНоменклатуры { get; set; }
        //public string КодПоКТРУ { get; set; }
        //public string КодТНВЭД { get; set; }
        //public string СтранаПроисхожденияКод { get; set; }
        //public bool ПрослеживаемыйТовар { get; set; }
        //public bool МаркируемыйТовар { get; set; }
        //public string ХешПравилаПоиска { get; set; }
        //public Другиештрихкодыноменклатуры[] ДругиеШтрихкодыНоменклатуры { get; set; }
        //public bool Predefined { get; set; }
        //public string PredefinedDataName { get; set; }
        //public string ВладелецНоменклатурыnavigationLinkUrl { get; set; }
        //public string УпаковкаnavigationLinkUrl { get; set; }
    }

    //public class Другиештрихкодыноменклатуры
    //{
    //    public string Ref_Key { get; set; }
    //    public string LineNumber { get; set; }
    //    public string Штрихкод { get; set; }
    //}


}
