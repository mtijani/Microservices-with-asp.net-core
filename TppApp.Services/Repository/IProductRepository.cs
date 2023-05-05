using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TppApp.Services.ProductAPI.models.Dtos;

namespace TppApp.Services.ProductAPI.Repository
{
    public interface IProductRepository
    {
        // Task because we want it to be async
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProductById(int ProductId);
        Task<ProductDto> CreateUpdateProduct(ProductDto productDto);
        Task<bool> DeleteProduct(int productId);
    }
}
