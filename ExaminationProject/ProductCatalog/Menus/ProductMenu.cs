using SharedClassLibrary.Interfaces;
using SharedClassLibrary.Models;
using SharedClassLibrary.Services;

namespace ProductCatalog.Menus;
internal class ProductMenu
{
    IProductService<Product, Product> productService = new ProductService(@"C:\projectsNackademin\bygg om mig\Senaste fungerande skolinlämning productcatalog\ExaminationProject\jsonfiler\produktlista.json");

    public void CreateNewProduct()
    {
        Product product = new Product();
        //Category category = new Category();

        Console.Clear();
        Console.WriteLine("--- Create New Product ---\n");

        Console.Write("Enter Product Name: ");
        product.ProductName = Console.ReadLine() ?? "";

        Console.Write("Enter Product Price: ");
        product.PriceDecimalTest = Console.ReadLine() ?? ""; // jag vet, jag behöver inte ha denna som en egenskap i ett objekt... men det vart så

        var answer = productService.AddProduct(product);
        Console.WriteLine(answer.Response);
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }


    public void ViewAllProducts()
    {

        var answer = productService.ShowAllProducts();
        Console.Clear();
        Console.WriteLine("--- Product List ---\n");
            /*
             *          CHATGPT - TABELLLIKNANDE TEXTFORMAT (med lite egna modifieringar)
             */
        Console.WriteLine("{0, -8} {1, -10} {2, -20} {3, -25}", "Index", "ID", "Name", "Price");
        Console.WriteLine(new string('-', 82));

        if (answer.Statement)
        {
            int productIndex = 0;
            foreach (Product product in answer.Content!)
            {
                // Product details formatted - CHATGPT (med lite egna modifieringar)
                Console.WriteLine("{0, -8} {1, -10} {2, -20} {3, -25:C} \n", productIndex, product.ProductId, product.ProductName, product.ProductPrice);
                productIndex++;
            }
        }
        else
        {
            Console.WriteLine(answer.Response);
            
        }

        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }
    public void DeleteProduct()
    {
        ViewAllProducts();
        Console.Write("\n\nChoose product to delete by index: ");
        string product = Console.ReadLine() ?? "";


        var answer = productService.RemoveProduct(product);
        Console.WriteLine(answer.Response);
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }

}
