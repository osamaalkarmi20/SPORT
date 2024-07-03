namespace SPORT.Models
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }
    }
}
