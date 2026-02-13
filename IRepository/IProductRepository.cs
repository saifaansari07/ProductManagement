using Microsoft.Identity.Client;
using ProductWebApi.Models;

namespace ProductWebApi.IRepository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetApprovedProductsAsync();

        Task<IEnumerable<Product>> GetPendingProductsAsync();

        Task<IEnumerable<Product>> GetApprovedAndPendingProductsAsync();

        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<IEnumerable<Product>> GetProductsForUserAsync(string role);
        Task<Product> GetApprovedByIdAsync(int id);
        Task<Product> GetSellerProductByIdAsync(int id);

        Task<Product> GetByByIdAsync(int id);

        Task<Product> GetProductByIdForUserAsync(int id, string role);
        Task AddProduct(Product product);

        Task<bool> UpdateProduct(Product product);

        Task DeleteProduct(int id);

        Task<Product> ApproveProduct(int id);

    }
}
