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
    public class CategoryApiManager : ICategoryApiService
    {
        private readonly HttpClient _httpClient;
        public CategoryApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<CategoryListViewModel>> GetAllAsync()
        {
          HttpResponseMessage httpResponseMessage= await _httpClient.GetAsync("http://localhost:64281/api/categories");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
              return JsonConvert.DeserializeObject<List<CategoryListViewModel>>(await httpResponseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

       

        public async Task<List<CategoryListViewModel>> GetCategoriesWithBlogCount()
        {
           var responseMessage= await _httpClient.GetAsync("http://localhost:64281/api/categories/CategoriesCountBlogs");
            if (responseMessage.IsSuccessStatusCode)
            {
               return JsonConvert.DeserializeObject<List<CategoryListViewModel>> (await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<Category> GetCategoryNameByIdAsync(int id)
        {
           var responseMessage= await _httpClient.GetAsync("http://localhost:64281/api/categories/"+id);

            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Category>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }
    }
}
