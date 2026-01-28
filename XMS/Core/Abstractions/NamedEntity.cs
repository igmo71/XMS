namespace XMS.Core.Abstractions
{
    public class NamedEntity : Entity, IHasName
    {
        public string Name { get; set; } = string.Empty;
    }
}
