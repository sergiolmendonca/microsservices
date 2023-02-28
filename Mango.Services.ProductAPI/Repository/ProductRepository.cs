using AutoMapper;
using Mango.Services.ProductAPI.DbContexts;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateUpdateProduct(ProductDto product)
        {
            Product prod = _mapper.Map<ProductDto, Product>(product);
            if (prod.ProductId > 0)
            {
                _db.Products.Update(prod);
            }
            else
            {
                _db.Products.Add(prod);
            }

            await _db.SaveChangesAsync();

            return _mapper.Map<Product, ProductDto>(prod);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                Product prod = await _db.Products.SingleOrDefaultAsync(x => x.ProductId== id);
                if (prod != null) throw new Exception();
                
                _db.Products.Remove(prod);
                _db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            Product product = await _db.Products
                .Where(x => x.ProductId == id).FirstOrDefaultAsync();

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            IList<Product> products = await _db.Products.ToListAsync();

            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
