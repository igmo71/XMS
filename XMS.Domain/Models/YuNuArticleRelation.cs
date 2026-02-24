using System.Text.Json;
using System.Text.Json.Serialization;

namespace XMS.Domain.Models
{
    public class YuNuArticleRelation
    {
        [JsonPropertyName("id")]
        public long? Id { get; set; }

        [JsonPropertyName("article_id")]
        public long? ArticleId { get; set; }

        [JsonPropertyName("related_article_id")]
        public long? RelatedArticleId { get; set; }

        [JsonPropertyName("relation_type")]
        public string? RelationType { get; set; }

        [JsonExtensionData]
        public Dictionary<string, JsonElement> ExtraData { get; set; } = [];
    }
}
