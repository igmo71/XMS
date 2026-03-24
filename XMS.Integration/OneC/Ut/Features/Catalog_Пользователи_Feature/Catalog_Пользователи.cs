using System.ComponentModel.DataAnnotations;
using XMS.Integration.OneC.Abstractions;

namespace XMS.Integration.OneC.Ut.Features.Catalog_Пользователи_Feature
{
    internal class Catalog_Пользователи : IOneCCatalog
    {
        public Guid Ref_Key { get; set; }
        [MaxLength(OneCSettings.CODE)] public string? DataVersion { get; set; }
        [MaxLength(OneCSettings.DESCRIPTION)] public string? Description { get; set; }
        public bool DeletionMark { get; set; }

        public static string Uri => "Catalog_Пользователи?$format=json&$select=Ref_Key,Description,DeletionMark&$inlinecount=allpages";

        public static string GetUriByRefKey(Guid refKey) => $"{Uri}&$filter=Ref_Key eq guid'{refKey}'";

        public static string QueueName => nameof(Catalog_Пользователи);
    }
}
