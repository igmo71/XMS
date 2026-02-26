namespace XMS.Infrastructure.Integration.OneS.BuhGodoo.Domain
{
    public class Catalog_Номенклатура
    {
        public Guid Ref_Key { get; set; }
        public string? Description { get; set; }
        public bool IsFolder { get; set; }
        public string? Code { get; set; }
        public string? Артикул { get; set; }
        public static string Uri => "Catalog_Номенклатура?$format=json&$inlinecount=allpages&$select=Ref_Key,Description,IsFolder,Code,Артикул";
    }

}
