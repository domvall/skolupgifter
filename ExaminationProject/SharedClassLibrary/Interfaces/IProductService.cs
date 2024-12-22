using SharedClassLibrary.Models;

namespace SharedClassLibrary.Interfaces;

public interface IProductService<T, TResult> where T : class where TResult : class
{           
    public AnswerOutcome<Product> AddProduct(Product product);
    public AnswerOutcome<Product> ShowOneProduct(string productName);
    public AnswerOutcome<IEnumerable<Product>> ShowAllProducts();
    public AnswerOutcome<Product> EditProduct(string productName, Product modifiedProduct);
    public AnswerOutcome<Product> RemoveProduct(string productName);
}
