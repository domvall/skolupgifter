using Newtonsoft.Json;
using SharedClassLibrary.Models;
using SharedClassLibrary.Services;
using System.Text.Json;

namespace SharedClassLibrary.Tests;

public class ProductServiceTests
{
    [Fact]
    public void AddProductacc_ShouldAddProductToList_And_ReturnTrue__Else__FalseIfProductAlreadyExists()
    {
        //Arrange
        File.WriteAllText("testfil-produkter.json", "[]");  //rensar testfilen innan varje test, så testet kommer alltid lyckas sålänge sparandet till testfilen lyckas
                                                            //tas denna rad bort kommer testet enbart lyckas första gången, sedan misslyckas då produkten redan är sparad och existerar
                                                            //rebuild solution nollställer i sånt fall och testet kommer lyckas igen

        Product product = new Product { ProductName = "Test", PriceDecimalTest = "1" };

        
        ProductService productService = new ProductService("testfil-produkter.json");
        


        //Act
        AnswerOutcome<Product> result = productService.AddProduct(product);

        //Assert
        
            Assert.True(result.Statement);
            Assert.Equal("\n[Success] Productlist have been saved to file.", result.Response);



    }

    [Fact]
    /*
        CHATGPT fick lov att hjälpa till här, efter mycket om och men och modifieringar av AddProduct() i ProductService lyckades testet.
        Har kommenterat ut den gamla lösningen i AddProduct() metoden för att kunna jämföra. felet ligger dock i hur decimalerna hanteras i produktpriset
     */
    public void ShowAllProducts_ShouldMatchProductsInFile()
    {
        // Arrange
        string filePath = "testfil-produkter.json";
        List<Product> expectedProducts = new List<Product>
    {
        new Product { ProductName = "Test", PriceDecimalTest = "1" }
    };

        // Skriv förväntade produkter till filen
        File.WriteAllText(filePath, JsonConvert.SerializeObject(expectedProducts, Formatting.Indented));
        ProductService productService = new ProductService(filePath);

        // Act
        var result = productService.ShowAllProducts();

        // Assert
        Assert.True(result.Statement, $"ShowAllProducts failed: {result.Response}");
        Assert.NotNull(result.Content); // Kontrollera att produkter har hämtats

        // Jämför produkterna, men tillåt en viss precision för decimalvärden
        foreach (var expectedProduct in expectedProducts)
        {
            var actualProduct = result.Content.FirstOrDefault(p => p.ProductName == expectedProduct.ProductName);
            Assert.NotNull(actualProduct);
            Assert.Equal(expectedProduct.ProductName, actualProduct.ProductName);
            Assert.Equal(expectedProduct.PriceDecimalTest, actualProduct.PriceDecimalTest);
            Assert.InRange(actualProduct.ProductPrice, expectedProduct.ProductPrice - 0.01m, expectedProduct.ProductPrice + 0.01m);  // Jämför med liten marginal för decimaler
        }
    }
}
