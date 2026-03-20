using System.ComponentModel.DataAnnotations;
using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC.Ut.Models
{
    internal class Catalog_Пользователи : IOneCCatalog
    {
        public Guid Ref_Key { get; set; }
        [MaxLength(OneCSettings.DESCRIPTION)] public string? Description { get; set; }
        public bool DeletionMark { get; set; }
        public static string Uri => "Catalog_Пользователи?$format=json&$select=Ref_Key,Description,DeletionMark&$inlinecount=allpages";
    }
}
