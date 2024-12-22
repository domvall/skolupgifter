namespace SharedClassLibrary.Models;

public class Product
{

    public string ProductId { get; set; } = Guid.NewGuid().ToString().Substring(0, 5);
    public string ProductName { get; set; } = null!;
    public decimal ProductPrice { get; set; }
    public string PriceDecimalTest { get; set; } = null!;
    public string ProductCategory { get; set; } = null!;

}
