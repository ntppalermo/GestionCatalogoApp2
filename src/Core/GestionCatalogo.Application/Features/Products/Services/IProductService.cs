using GestionCatalogo.Application.Features.Products.DTOs;

namespace GestionCatalogo.Application.Features.Products.Services;

public interface IProductService
{
    Task<IReadOnlyList<ProductDto>> GetAllProductsAsync();
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto);
    Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto updateProductDto);
    Task<bool> DeleteProductAsync(int id);
    Task<IReadOnlyList<ProductDto>> GetActiveProductsAsync();
    Task<IReadOnlyList<ProductDto>> GetProductsByCategoryAsync(string category);
}