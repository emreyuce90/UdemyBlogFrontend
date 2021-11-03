using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UdemyBlogFrontend.ApiServices.Interfaces;
using UdemyBlogFrontend.Models;

namespace UdemyBlogFrontend.ApiServices.Concrete
{
    public class CategoryApiManager : ICategoryApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CategoryApiManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateCategoryAsync(CategoryList category)
        {
            MultipartFormDataContent formDataContent = new MultipartFormDataContent();
            formDataContent.Add(new StringContent(category.Name), nameof(CategoryList.Name));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));
            await _httpClient.PostAsync("http://localhost:64281/api/categories", formDataContent);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));
            await _httpClient.DeleteAsync("http://localhost:64281/api/categories/" + id);

        }

        public async Task<List<CategoryListViewModel>> GetAllAsync()
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("http://localhost:64281/api/categories");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<CategoryListViewModel>>(await httpResponseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<List<CategoryList>> GetAllCategoriesAsync()
        {
            var responseMessage = await _httpClient.GetAsync("http://localhost:64281/api/categories");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<CategoryList>>(await responseMessage.Content.ReadAsStringAsync());

            }
            return null;
        }

        public async Task<List<CategoryListViewModel>> GetCategoriesWithBlogCount()
        {
            var responseMessage = await _httpClient.GetAsync("http://localhost:64281/api/categories/CategoriesCountBlogs");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<CategoryListViewModel>>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<CategoryList> GetCategoryByCategoryIdAsync(int id)
        {
            var responseMessage = await _httpClient.GetAsync("http://localhost:64281/api/categories/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<CategoryList>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<Category> GetCategoryNameByIdAsync(int id)
        {
            var responseMessage = await _httpClient.GetAsync("http://localhost:64281/api/categories/" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Category>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task UpdateCategoryAsync(CategoryList category)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();
            formData.Add(new StringContent(category.Id.ToString()), nameof(CategoryList.Id));
            formData.Add(new StringContent(category.Name), nameof(CategoryList.Name));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));
            await _httpClient.PutAsync("http://localhost:64281/api/categories/" + category.Id, formData);
        }
    }
}
