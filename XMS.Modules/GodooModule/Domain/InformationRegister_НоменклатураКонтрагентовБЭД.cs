namespace XMS.Modules.GodooModule.Domain
{
    internal class InformationRegister_НоменклатураКонтрагентовБЭД
    {
        public string? Владелец_Key { get; set; }
        public string? Номенклатура { get; set; }
        public string? Номенклатура_Type { get; set; }
        public string? Наименование { get; set; }
        public string? Идентификатор { get; set; }
        public string? Артикул { get; set; }
        public string? Штрихкод { get; set; }

        internal static string GetUri(string? ownerKey, string? identifier)
        {
            string uri = $"InformationRegister_НоменклатураКонтрагентовБЭД?$format=json&$inlinecount=allpages" +
                $"&$filter=Владелец_Key eq guid'{ownerKey}' and Идентификатор eq '{identifier}'";
            return uri;
        }

        //public string? ЕдиницаИзмерения { get; set; }
        //public string НаименованиеХарактеристики { get; set; }
        //public string ЕдиницаИзмеренияКод { get; set; }        
        //public string СтавкаНДС { get; set; }        
        //public string ИдентификаторНоменклатурыСервиса { get; set; }
        //public string ИдентификаторХарактеристикиСервиса { get; set; }
        //public string Номенклатура_Type { get; set; }
        //public string Характеристика_Key { get; set; }
        //public string Упаковка_Key { get; set; }
        //public string ИдентификаторНоменклатуры { get; set; }
        //public string ИдентификаторХарактеристики { get; set; }
        //public string ИдентификаторУпаковки { get; set; }
        //public bool ИспользоватьХарактеристики { get; set; }
        //public string ШтрихкодКомбинации { get; set; }
        //public string ШтрихкодыНоменклатуры { get; set; }
        //public string НаименованиеУпаковки { get; set; }
        //public string ТипНоменклатурыФНС { get; set; }
        //public string КодНоменклатуры { get; set; }
        //public string КодПоКТРУ { get; set; }
        //public string КодТНВЭД { get; set; }
        //public string СтранаПроисхожденияКод { get; set; }
        //public bool ПрослеживаемыйТовар { get; set; }
        //public bool МаркируемыйТовар { get; set; }
        //public string ХешПравилаПоиска { get; set; }
        //public string ХешНаименования { get; set; }
        //public string ХешАртикула { get; set; }
        //public string ХешКодаНоменклатуры { get; set; }
        //public string ХешШтрихкода { get; set; }
    }
}
