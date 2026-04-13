namespace XMS.Integration.OneC.Abstractions;

public interface ICatalog : IOdataEntity
{
    string? Description { get; set; }
}
