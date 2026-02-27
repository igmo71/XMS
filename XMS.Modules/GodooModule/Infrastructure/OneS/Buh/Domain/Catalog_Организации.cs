namespace XMS.Modules.GodooModule.Infrastructure.OneS.Buh.Domain
{
    internal class Catalog_Организации
    {
        public Guid Ref_Key { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public static string Uri => "Catalog_Организации?$format=json&$inlinecount=allpages&$select=Ref_Key,Description,Code";
    }
}
