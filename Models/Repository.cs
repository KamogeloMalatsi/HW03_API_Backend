using Microsoft.EntityFrameworkCore;

namespace Assignment3_Backend.Models
{
    public class Repository:IRepository
    {
        private readonly AppDbContext _appDbContext;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add<T>(T entity) where T : class
        {
            _appDbContext.Add(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync() > 0;
        }
        public async Task<Brand[]> GetBrandsAsync()
        {
            IQueryable<Brand> query = _appDbContext.Brands;
            return await query.ToArrayAsync();
        }
        public async Task<ProductType[]> GetProductTypesAsync()
        {
            IQueryable<ProductType> query = _appDbContext.ProductTypes;
            return await query.ToArrayAsync();
        }
        public async Task<Product[]> GetProductsAsync()
        {
            IQueryable<Product> query = _appDbContext.Products.Include(p => p.Brand).Include(p => p.ProductType);
            return await query.ToArrayAsync();
        }
    }
}
