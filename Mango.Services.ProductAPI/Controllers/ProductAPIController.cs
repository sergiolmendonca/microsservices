using Mango.Services.ProductAPI.Models.Dto;
using Mango.Services.ProductAPI.Models.Dtos;
using Mango.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/products")]
    public class ProductAPIController : ControllerBase
    {
        protected IResponseDto _response;
        private IProductRepository _productRepository;

        public ProductAPIController(IResponseDto response, IProductRepository productRepository)
        {
            _response = response;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<ProductDto> response = await _productRepository.GetProducts();
                _response.Result = response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }

            return _response;

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                ProductDto response = await _productRepository.GetProductById(id);
                _response.Result = response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }

            return _response;

        }

        [HttpPost]
        public async Task<object> Post([FromBody] ProductDto product)
        {
            try
            {
                if (product.ProductId > 0) throw new Exception("The Id already exists.");
                ProductDto response = await _productRepository.CreateUpdateProduct(product);
                _response.Result = response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }

            return _response;
        }

        [HttpPut]
        public async Task<object> Put([FromBody] ProductDto product)
        {
            try
            {
                if (product.ProductId < 1) throw new Exception("Id not given.");
                ProductDto response = await _productRepository.CreateUpdateProduct(product);
                _response.Result = response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }

            return _response;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                if (id < 1) throw new Exception("Id not given.");
                bool response = await _productRepository.DeleteProduct(id);

                _response.Result = "Product deleted.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }

            return _response;
        }
    }
}
