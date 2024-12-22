using Newtonsoft.Json;
using SharedClassLibrary.Interfaces;
using SharedClassLibrary.Models;
using System.Globalization;

namespace SharedClassLibrary.Services;

public class ProductService : IProductService<Product, Product>
{


    private readonly IFileService _fileService;
    private List<Product> _productlist;
    public ProductService(string filePath)  //konstruktor
    {
        _fileService = new FileService(filePath);
        _productlist = new List<Product>();
        ShowAllProducts();
    }


    //public AnswerOutcome<Product> AddProduct(Product product)
    //{
    //    bool priceResult = decimal.TryParse(product.PriceDecimalTest, out decimal price);
    //    bool nameResult = product.ProductName.All(char.IsLetter);
    //    try
    //    {
    //        if (!string.IsNullOrEmpty(product.ProductName) && priceResult == true && nameResult == true && product.PriceDecimalTest != "0") //kontrollera om namnet redan existerar i produktlistan
    //        {
    //            if (!_productlist.Any(pl => pl.ProductName == product.ProductName))
    //            {
    //                product.ProductPrice = price;
    //                _productlist.Add(product);
    //                var json = JsonConvert.SerializeObject(_productlist, Formatting.Indented);
    //                var fileResult = _fileService.SaveToFile(json);
    //                if (fileResult.Statement)
    //                {
    //                    return new AnswerOutcome<Product> { Statement = true, Response = "\n[Success] Productlist have been saved to file." };
    //                }
    //                else
    //                {
    //                    return new AnswerOutcome<Product> { Statement = false, Response = "\n[Error] Productlist Was not saved to file." };
    //                }
    //            }
    //            /*
    //             * else if (string.(product.ProductCategory))
    //            {
    //                return new AnswerOutcome<Product> { Statement = false, Response = $"\n[Error] only numbers within the index of the categorylist is allowed."};
    //            }*/
    //            else
    //            {
    //                return new AnswerOutcome<Product> { Statement = false, Response = ErrorMessage() };
    //                }
    //        }
    //        else
    //        {
    //            return new AnswerOutcome<Product> { Statement = false, Response = ErrorMessage() };
    //        }

    //    }

    //    catch (Exception ex)
    //    {
    //        return new AnswerOutcome<Product> { Statement = false, Response = ex.Message };
    //    }
    //}

    public AnswerOutcome<Product> AddProduct(Product product)
    {
        // Försök att omvandla PriceDecimalTest till ett decimalvärde
        bool priceResult = decimal.TryParse(product.PriceDecimalTest, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price);
        bool nameResult = product.ProductName.All(char.IsLetter);

        try
        {
            if (!string.IsNullOrEmpty(product.ProductName) && priceResult && nameResult && product.PriceDecimalTest != "0")
            {
                // Kontrollera att produktens namn inte redan finns
                if (!_productlist.Any(pl => pl.ProductName == product.ProductName))
                {
                    product.ProductPrice = price;  // Sätt den konverterade decimalen till ProductPrice
                    _productlist.Add(product);
                    var json = JsonConvert.SerializeObject(_productlist, Formatting.Indented);
                    var fileResult = _fileService.SaveToFile(json);

                    if (fileResult.Statement)
                    {
                        return new AnswerOutcome<Product> { Statement = true, Response = "\n[Success] Productlist have been saved to file." };
                    }
                    else
                    {
                        return new AnswerOutcome<Product> { Statement = false, Response = "\n[Error] Productlist Was not saved to file." };
                    }
                }
                else
                {
                    return new AnswerOutcome<Product> { Statement = false, Response = ErrorMessage() };
                }
            }
            else
            {
                return new AnswerOutcome<Product> { Statement = false, Response = ErrorMessage() };
            }
        }
        catch (Exception ex)
        {
            return new AnswerOutcome<Product> { Statement = false, Response = ex.Message };
        }
    }

    public AnswerOutcome<IEnumerable<Product>> ShowAllProducts()
    {
        var fileResult = _fileService.GetFromFile();


        try
        {
            
            if (fileResult.Statement)
            {
                _productlist = JsonConvert.DeserializeObject<List<Product>>(fileResult.Content!)!;
                return new AnswerOutcome<IEnumerable<Product>> { Statement = true, Content = _productlist };
            }
            else
            {
                return new AnswerOutcome<IEnumerable<Product>> { Statement = false, Response = fileResult.Response + "\n\nFile will be created automatically once you add a product." };
            }
            //else
            //{
            //    return new AnswerOutcome<IEnumerable<Product>> { Statement = false, Response = "[Note] There is no products in the list." };
            //}

        }
        catch (Exception ex)
        {
            return new AnswerOutcome<IEnumerable<Product>> { Statement = false, Response = ex.Message };
        }
    }

    public AnswerOutcome<Product> ShowOneProduct(string productname)
    {
        Product product = _productlist.FirstOrDefault(x => x.ProductName == productname)!;
        if (product != null)
        {

            return new AnswerOutcome<Product> { Statement = true, Response = $"{product.ProductId} : {product.ProductCategory} : {product.ProductName} : {product.ProductPrice}.", Content = product };

        }
        else
        {
            return new AnswerOutcome<Product> { Statement = false, Response = "There is no products with that name in the list." };
        }


    }

    public AnswerOutcome<Product> RemoveProduct(string input)
    {
        bool result = int.TryParse(input, out int index);

        try
        {
            if (result)
            {
                _productlist.RemoveAt(index);
                var json = JsonConvert.SerializeObject(_productlist, Formatting.Indented);
                var fileResult = _fileService.SaveToFile(json);
                return new AnswerOutcome<Product> { Statement = true, Response = $"\n[Success] Product was removed from file." };
            }
            else
            {
                return new AnswerOutcome<Product> { Statement = false, Response = $"\n[Error] only numbers between 0 - {_productlist.Count - 1} allowed." };
            }
        }
        catch (Exception ex)
        {
            return new AnswerOutcome<Product> { Statement = false, Response = ex.Message };
        }

    }


    AnswerOutcome<Product> IProductService<Product, Product>.EditProduct(string productName, Product modifiedProduct)
    {
        throw new NotImplementedException();
    }
    public string ErrorMessage()
    {

        string errorMessage = "\n[ERROR] Incorrect Values!\n" +
                              "\n   Productname can only contain letters and must be unique.\n" +
                              "\n   Productprice can only contain decimal and integers.\n" +
                              "\n   No fields can be left blank. Product has not been saved to the list.\n\n";
        return errorMessage;
    }
}


