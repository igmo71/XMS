using XMS.Common.SharedKernel;

namespace WMS.Project.Core.Domain;

public record Dimensions(double Height, double Width, double Depth, double Weight) : ValueObject
{
    public double Volume => Height * Width * Depth;
}
