namespace GestionCatalogo.Domain.Entities;

public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? Category { get; private set; }
    public string? Brand { get; private set; }
    public string? SKU { get; private set; }
    public bool IsActive { get; private set; } = true;

    public Product(string name, decimal price, int stock, string? description = null,
                   string? category = null, string? brand = null, string? sku = null, bool isActive = true)
    {
        Validate(name, price, stock);
        Name = name;
        Price = price;
        Stock = stock;
        Description = description ?? string.Empty;
        Category = category;
        Brand = brand;
        SKU = sku;
        IsActive = isActive;
        CreatedAt = DateTime.UtcNow;
    }

    private Product() { }

    public void Update(string name, decimal price, int stock, string? description = null,
                       string? category = null, string? brand = null, string? sku = null, bool? isActive = null)
    {
        Validate(name, price, stock);
        Name = name;
        Price = price;
        Stock = stock;
        Description = description ?? Description;
        Category = category;
        Brand = brand;
        SKU = sku;
        if (isActive.HasValue) IsActive = isActive.Value;
        UpdatedAt = DateTime.UtcNow;
    }

	private static void Validate(string name, decimal price, int stock)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Name is required.", nameof(name));
		if (price < 0)
			throw new ArgumentOutOfRangeException(nameof(price), "Price must be greater than or equal to 0.");
		if (stock < 0)
			throw new ArgumentOutOfRangeException(nameof(stock), "Stock must be greater than or equal to 0.");
	}

}