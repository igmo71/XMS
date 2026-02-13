namespace XMS.Application.Abstractions
{
    public interface IDbContextFactoryProxy
    {
        IApplicationDbContext CreateDbContext();
    }
}
