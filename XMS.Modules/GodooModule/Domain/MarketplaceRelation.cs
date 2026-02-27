namespace XMS.Modules.GodooModule.Domain
{
    /// <summary>
    /// InformationRegister_НоменклатураМаркетплейсов
    /// </summary>
    public class MarketplaceRelation
    {
        /// <summary>
        /// Маркетплейс
        /// </summary>
        public required string Marketplace { get; set; }

        /// <summary>
        /// ИдентификаторТовара
        /// </summary>
        public required string MarketplaceSku { get; set; }

        /// <summary>
        /// Штрихкод
        /// </summary>
        public string? Barcode { get; set; }

        /// <summary>
        /// Номенклатура_Key
        /// </summary>
        public required string ProductId { get; set; }

        /// <summary>
        /// Организация_Key
        /// </summary>
        public string? CompanyId { get; set; }
    }
}
