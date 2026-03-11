namespace XMS.Modules.GodooModule.Infrastructure.Yunu
{
    internal class YunuClientConfig
    {
        public required string BaseAddress { get; set; }
        public required string ArticleRelations { get; set; }
        public required List<ApiKey> ApiKeys { get; set; }
    }

    public class ApiKey
    {
        public required string Name { get; set; }
        public required string Value { get; set; }
        public required string CompanyId { get; set; }
    }
}
