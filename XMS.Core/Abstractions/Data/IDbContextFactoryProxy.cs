namespace XMS.Core.Abstractions.Data
{
    public interface IDbContextFactoryProxy
    {
        IApplicationDbContext CreateDbContext();
    }
}
