using XMS.Common.SharedKernel;

namespace WMS.Project.Core.Domain;

public record Coordinates(int X, int Y, int Z) : ValueObject;
