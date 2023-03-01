using Mango.Web.Models;
using Mango.Web.Services.IServices;

namespace Mango.Web.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IResponseDto responseDto, IHttpClientFactory httpClient) : base(responseDto, httpClient)
        {
        }

        public async Task<T> CreateProductAsync<T>(ProductDto product)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = product,
                Url = SD.ProductAPIBase + "api/products",
                AccessToken = ""
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Data = null,
                Url = SD.ProductAPIBase + $"api/products/{id}",
                AccessToken = ""
            });
        }

        public async Task<T> GetAllProductsAsync<T>()
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Data = null,
                Url = SD.ProductAPIBase + "api/products",
                AccessToken = ""
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = null,
                Url = SD.ProductAPIBase + $"api/products/{id}",
                AccessToken = ""
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDto product)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = product,
                Url = SD.ProductAPIBase + "api/products",
                AccessToken = ""
            });
        }
    }
}
