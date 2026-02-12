namespace XMS.Web.Integration.OneS.Ut.Domain
{
    public class Catalog_Пользователи
    {
        public Guid Ref_Key { get; set; }
        public string? Description { get; set; }
        public bool DeletionMark { get; set; }
        public static string Uri => "Catalog_Пользователи?$format=json&$select=Ref_Key,Description,DeletionMark&$inlinecount=allpages";
    }
}
