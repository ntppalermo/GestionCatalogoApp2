using GestionCatalogo.Application.Features.Products.DTOs;
using GestionCatalogo.Domain.Entities;

public static class ProductMapping
{
	public static ProductDto ToDto(this Product product)
	{
		if (product is null) throw new ArgumentNullException(nameof(product));

		return new ProductDto
		{
			Id = product.Id,
			Name = product.Name,
			Description = product.Description,
			Price = product.Price,
			Stock = product.Stock,
			CreatedAt = product.CreatedAt,
			UpdatedAt = product.UpdatedAt
		};
	}

	public static Product ToEntity(this CreateProductDto dto)
	{
		if (dto is null) throw new ArgumentNullException(nameof(dto));

		return new Product(dto.Name, dto.Price, dto.Stock, dto.Description);
	}

	public static void UpdateFrom(this Product product, UpdateProductDto dto)
	{
		if (product is null) throw new ArgumentNullException(nameof(product));
		if (dto is null) throw new ArgumentNullException(nameof(dto));

		product.Update(dto.Name, dto.Price, dto.Stock, dto.Description);
	}
}