using NLayer.Core.Models;

namespace NLayer.Core.Repositories
{
    // Burada Product entitymize özel method yazıyoruz.
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetProductsWithCategory();
    }
}
