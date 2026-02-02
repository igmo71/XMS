namespace XMS.Core.Abstractions
{
    public class NamedEntity : BaseEntity, IHasName
    {
        public string Name { get; set; } = string.Empty;
    }
}
