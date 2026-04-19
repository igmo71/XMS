namespace XMS.Application.Abstractions.Data;

public interface IDbContextFactoryProxy
{
    IApplicationDbContext CreateDbContext();
}
