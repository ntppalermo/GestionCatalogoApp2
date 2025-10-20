using GestionCatalogo.Domain.Entities;

namespace GestionCatalogo.Application.Contracts.Persistence;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IReadOnlyList<Product>> GetActiveProductsAsync();
    Task<IReadOnlyList<Product>> GetProductsByCategoryAsync(string category);
    Task<Product?> GetProductBySkuAsync(string sku);
}