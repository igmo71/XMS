namespace XMS.Application.Abstractions.Integration.OneC;

public interface ICatalog : IOdataEntity
{
    string? Description { get; set; }
}
