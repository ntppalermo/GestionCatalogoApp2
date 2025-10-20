using GestionCatalogo.Application.Features.Products.DTOs;
using GestionCatalogo.Application.Features.Products.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionCatalogo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
	private readonly IProductService _productService;
	private readonly ILogger<ProductsController> _logger;

	public ProductsController(IProductService productService, ILogger<ProductsController> logger)
	{
		_productService = productService;
		_logger = logger;
	}

	/// <summary>
	/// Gets all products
	/// </summary>
	/// <returns>List of products</returns>
	[HttpGet]
	[ProducesResponseType(typeof(IReadOnlyList<ProductDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetProducts()
	{
		try
		{
			var products = await _productService.GetAllProductsAsync();
			return Ok(products);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error retrieving products");
			return StatusCode(500, "Internal server error");
		}
	}

	/// <summary>
	/// Gets a product by its ID
	/// </summary>
	/// <param name="id">Product ID</param>
	/// <returns>Found product</returns>
	[HttpGet("{id}")]
	[ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetProductById(int id)
	{
		try
		{
			if (id <= 0)
				return BadRequest("ID must be greater than 0");

			var product = await _productService.GetProductByIdAsync(id);
			if (product == null)
				return NotFound($"Product with ID {id} not found");

			return Ok(product);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error retrieving product with ID {ProductId}", id);
			return StatusCode(500, "Internal server error");
		}
	}

	/// <summary>
	/// Gets active products
	/// </summary>
	/// <returns>List of active products</returns>
	[HttpGet("active")]
	[ProducesResponseType(typeof(IReadOnlyList<ProductDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetActiveProducts()
	{
		try
		{
			var products = await _productService.GetActiveProductsAsync();
			return Ok(products);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error retrieving active products");
			return StatusCode(500, "Internal server error");
		}
	}

	/// <summary>
	/// Gets products by category
	/// </summary>
	/// <param name="category">Product category</param>
	/// <returns>List of products in the category</returns>
	[HttpGet("category/{category}")]
	[ProducesResponseType(typeof(IReadOnlyList<ProductDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetProductsByCategory(string category)
	{
		try
		{
			if (string.IsNullOrWhiteSpace(category))
				return BadRequest("Category cannot be empty");

			var products = await _productService.GetProductsByCategoryAsync(category);
			return Ok(products);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error retrieving products by category {Category}", category);
			return StatusCode(500, "Internal server error");
		}
	}

	/// <summary>
	/// Creates a new product
	/// </summary>
	/// <param name="createProductDto">Product data to create</param>
	/// <returns>Created product</returns>
	[HttpPost]
	[ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
	{
		try
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var product = await _productService.CreateProductAsync(createProductDto);
			return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error creating product");
			return StatusCode(500, "Internal server error");
		}
	}

	/// <summary>
	/// Updates an existing product
	/// </summary>
	/// <param name="id">Product ID</param>
	/// <param name="updateProductDto">Updated product data</param>
	/// <returns>Updated product</returns>
	[HttpPut("{id}")]
	[ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto updateProductDto)
	{
		try
		{
			if (id <= 0)
				return BadRequest("ID must be greater than 0");

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var product = await _productService.UpdateProductAsync(id, updateProductDto);
			if (product == null)
				return NotFound($"Product with ID {id} not found");

			return Ok(product);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error updating product with ID {ProductId}", id);
			return StatusCode(500, "Internal server error");
		}
	}

	/// <summary>
	/// Deletes a product
	/// </summary>
	/// <param name="id">Product ID</param>
	/// <returns>Operation result</returns>
	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> DeleteProduct(int id)
	{
		try
		{
			if (id <= 0)
				return BadRequest("ID must be greater than 0");

			var result = await _productService.DeleteProductAsync(id);
			if (!result)
				return NotFound($"Product with ID {id} not found");

			return NoContent();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error deleting product with ID {ProductId}", id);
			return StatusCode(500, "Internal server error");
		}
	}
}