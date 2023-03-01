using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace Mango.Web.Services
{
    public class BaseService : IBaseService
    {
        public IResponseDto ResponseDto { get; set; }
        public IHttpClientFactory HttpClient { get; set; }

        public BaseService(IResponseDto responseDto, IHttpClientFactory httpClient)
        {
            ResponseDto = responseDto;
            HttpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = HttpClient.CreateClient("MangoAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear();

                if (apiRequest.Data is null) throw new Exception("The sending Data is Empty.");

                message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), 
                    Encoding.UTF8, "application/json");

                HttpResponseMessage apiResponse = null;
                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(apiContent);
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto()
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string>() { ex.Message },
                    IsSuccess = false
                };

                var res = JsonConvert.SerializeObject(dto);

                return JsonConvert.DeserializeObject<T>(res);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
