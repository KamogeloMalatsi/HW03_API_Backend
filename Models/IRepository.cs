namespace Assignment3_Backend.Models
{
    public interface IRepository
    {
        Task<bool> SaveChangesAsync();
        
        void Add<T>(T entity) where T : class;

        Task<Brand[]> GetBrandsAsync();
        Task<ProductType[]> GetProductTypesAsync();
        Task<Product[]> GetProductsAsync();
    }
}
