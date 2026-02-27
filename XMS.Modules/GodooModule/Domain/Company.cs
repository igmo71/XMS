namespace XMS.Modules.GodooModule.Domain
{
    /// <summary>
    /// Catalog_Организации
    /// </summary>
    public class Company
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
        /// Code
        /// </summary>
        public string? Code { get; set; }
    }
}
