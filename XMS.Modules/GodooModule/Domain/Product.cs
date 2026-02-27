namespace XMS.Modules.GodooModule.Domain
{
    /// <summary>
    /// Catalog_Номенклатура
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Ref_Key
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Артикул
        /// </summary>
        public string? Sku { get; set; }    
    }
}
