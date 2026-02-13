using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductWebApi.IRepository;
using ProductWebApi.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository) { 
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var role  = User.FindFirst(ClaimTypes.Role)?.Value;

            var result =await _productRepository.GetProductsForUserAsync(role);
            return result!=null ? Ok(result) : NotFound();
            //return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var result = await _productRepository.GetProductByIdForUserAsync(id,role);
            return result!=null ? Ok(result) : NotFound();
            //return Ok(result);
        }

        [HttpPost]
        public  async Task<IActionResult> Add(Product product)
        {
            await _productRepository.AddProduct(product);
            return Ok("Product added successfully.");
        }

        [HttpPut("id")]
        public async Task<IActionResult>  Update(Product product)
        {
           var data =await  _productRepository.UpdateProduct(product);
            return data != null ? Ok("Product updated successfully") : NotFound();
        }

        [HttpDelete("{id}")]
        public  async Task<IActionResult> Delete(int id)
        {
            await _productRepository.DeleteProduct(id);
            return Ok("Product deleted successfully.");
        }
    }
}
