using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UdemyBlogFrontend.ApiServices.Interfaces;

namespace UdemyBlogFrontend.ApiServices.Concrete
{
    public class ImageApiManager : IImageApiService
    {
        private readonly HttpClient _httpClient;
        public ImageApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetBlogImageByIdAsync(int id)
        {
           HttpResponseMessage responseMessage= await _httpClient.GetAsync($"http://localhost:64281/api/Images/GetBlogImageById/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
               var bytes =await responseMessage.Content.ReadAsByteArrayAsync();
                return $"data:image/jpeg;base64,{Convert.ToBase64String(bytes)}";
            }
            return null;
        }
    }
}
