using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ProductWebApi.Data;
using ProductWebApi.IRepository;
using ProductWebApi.Models;
using System.Reflection.Metadata.Ecma335;

namespace ProductWebApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddProduct(Product product)
        {
            await _context.Products.AddAsync( new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                CreatedAt = DateTime.UtcNow,
                ApprovalStatus="Pending"
            });
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var getById = await _context.Products.FindAsync(id);
            if (getById != null)
            {
              _context.Products.Remove(getById);
              await  _context.SaveChangesAsync(); 
            }
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetApprovedAndPendingProductsAsync()
        {
            return await _context.Products.Where(p=>p.ApprovalStatus!="Rejected").ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetApprovedProductsAsync()
        {
             return await _context.Products.Where(p=>p.ApprovalStatus=="Approved").ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetPendingProductsAsync()
        {
            return await _context.Products.Where(p=>p.ApprovalStatus=="Pending").ToListAsync() ;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var getbyId = await _context.Products.FirstOrDefaultAsync(x=>x.ProductId == product.ProductId);
            if (getbyId == null)
                return false;
            getbyId.Name = product.Name;
            getbyId.Description = product.Description;
            getbyId.Price = product.Price;
            getbyId.Quantity = product.Quantity;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> GetProductsForUserAsync(string role)
        {
            return role switch
            {
                "Customer" => await GetApprovedProductsAsync(),

                "ProductSeller" => await GetApprovedAndPendingProductsAsync(),

                "Admin" => await GetPendingProductsAsync(),

                _ => await GetAllProductsAsync()
            };
        }

        public async Task<Product> GetApprovedByIdAsync(int id)
        {
            return await _context.Products
                 .FirstOrDefaultAsync(p => p.ProductId == id && p.ApprovalStatus == "Approved") ??
                 throw new KeyNotFoundException("Product not found");
        }

        public async Task<Product> GetSellerProductByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id && p.ApprovalStatus != "Rejected");
        }

        public async Task<Product> GetByByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> GetProductByIdForUserAsync(int id,string role)
        {
            return role switch
            {
                "Customer" => await GetApprovedByIdAsync(id),

                "ProductSeller" => await GetSellerProductByIdAsync(id),

                "Admin" => await GetByByIdAsync(id),

                _=> throw new KeyNotFoundException($"Product with id {id} is not found.")
            };
        }

        public Task<Product> ApproveProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}
