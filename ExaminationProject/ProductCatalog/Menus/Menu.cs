namespace ProductCatalog.Menus;

internal class Menu
{
    private readonly ProductMenu _productMenu = new ProductMenu(); // instansierar Produktmenyn
    
    public void MainMenu()
    {
        Console.Clear();
        Console.WriteLine("--- Main Menu ---\n");
        Console.WriteLine("[1] Create New Product");
        Console.WriteLine("[2] View All Products");
        Console.WriteLine("[3] Delete Product");
        Console.WriteLine("[0] Exit Applicationn\n");

        Console.Write("Enter Your Choice: ");
        var menuInput = Console.ReadLine();

        switch (menuInput)
        {
            case "1":
                _productMenu.CreateNewProduct();
                break;
            case "2":
                _productMenu.ViewAllProducts();
                break;
            case "3":
                _productMenu.DeleteProduct();
                break;
            case "0":
                ExitApplication();
                break;
            default:
                Console.WriteLine("\n[Error] you can only choose a number between 0 - 3, try again.");
                Console.ReadKey();
                break;

        }
    }
    public void ExitApplication()
    {
        Environment.Exit(0);
    }
}
