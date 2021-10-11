using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UdemyBlogFrontend.ApiServices.Interfaces;
using UdemyBlogFrontend.Models;

namespace UdemyBlogFrontend.ApiServices.Concrete
{
    public class BlogApiManager : IBlogApiService
    {
        private readonly HttpClient _httpClient;
        public BlogApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<BlogList>> GetAllAsync()
        {
            var responseMessage = await _httpClient.GetAsync("http://localhost:64281/api/blogs");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogList>>(await responseMessage.Content.ReadAsStringAsync());

            }
            return null;
        }

        

        public async Task<BlogList> GetBlogDetailByIdAsync(int Id)
        {
            //Id ye göre api çekme
            HttpResponseMessage responseMessage = await _httpClient.GetAsync("http://localhost:64281/api/blogs/" + Id);


            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BlogList>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<List<BlogList>> GetBlogsByCategoryId(int Id)
        {
            var responseMessage=await _httpClient.GetAsync("http://localhost:64281/api/blogs/GetBlogsWithCategories/"+Id);

            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogList>>(await responseMessage.Content.ReadAsStringAsync());
            }

            return null;
        }
    }
}
