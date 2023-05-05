using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TppApp.Services.ProductAPI.DbContexts;
using TppApp.Services.ProductAPI.models;
using TppApp.Services.ProductAPI.models.Dtos;

namespace TppApp.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
            public ProductRepository(ApplicationDbContext db , IMapper mapper)
        {
            _db = db;
            _mapper = mapper;


                
        }
        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            Product product = _mapper.Map<ProductDto, Product>(productDto);
            if(product.ProductId > 0)
            {
                _db.Products.Update(product);
            }
            else
            {
                _db.Products.Add(product);
            }
          await  _db.SaveChangesAsync();
            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                Product product = await _db.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
                if(product == null)
                {
                    return false;
                }
                _db.Products.Remove(product);
                 await _db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ProductDto> GetProductById(int ProductId)
        {
            Product Product = await  _db.Products.Where(x=>x.ProductId == ProductId).FirstOrDefaultAsync();
            // COnvert product list to productDto 
            return _mapper.Map<ProductDto>(Product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            IEnumerable < Product > ProductList = await _db.Products.ToListAsync();
            // COnvert product list to productDto 
            return _mapper.Map<List<ProductDto>>(ProductList);
            
        }
    }
}
