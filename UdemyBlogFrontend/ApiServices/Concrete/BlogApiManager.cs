using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UdemyBlogFrontend.ApiServices.Interfaces;
using UdemyBlogFrontend.CustomExtensions;
using UdemyBlogFrontend.Models;

namespace UdemyBlogFrontend.ApiServices.Concrete
{
    public class BlogApiManager : IBlogApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlogApiManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            

        }


        //Backend tarafına formumuzu post edeceğiz

        public async Task AddBlogAsync(BlogAddModel model)
        {
            //İlk olarak form göndereceğimiz için formcontent sınıfından bir örnek alalım
            MultipartFormDataContent formData = new MultipartFormDataContent();



            //userid //fotoğraf //ve kalan datayı form olarak post edeceğiz
            if (model.File != null)
            {
                //modelde image kısmı dolu ise bu image in filename ini ilk olarak byte olarak okuyalım
                var stream = new MemoryStream();
                await model.File.CopyToAsync(stream);
                var bytes = stream.ToArray();
               
                
        
                //okuduğumuz bu bytlerı isteğimizin headers kısmında geçeceğiz.
                ByteArrayContent byteContent = new ByteArrayContent(bytes);
                //headers n content type ını gidecek olan görselin content type ını verdik
                byteContent.Headers.ContentType = new MediaTypeHeaderValue(model.File.ContentType);

                //form dataya bytearray olarak okuduğumuz datayı,Image ,Image dosya adını verdik
                formData.Add(byteContent,nameof(BlogAddModel.File),model.File.FileName);
            }



            //appuser id yi session kısmından çekmemiz gerekir.Daha önce setstringini activeUser olarak geçmiştik
            var user =_httpContextAccessor.HttpContext.Session.GetObject<ActiveUser>("activeUser");
            model.AppUserId = user.Id;


            formData.Add(new StringContent(model.AppUserId.ToString()), nameof(BlogAddModel.AppUserId));
            //artık geri kalan modellerimizi stringContent olarak geçebiliriz
            formData.Add(new StringContent(model.Description), nameof(BlogAddModel.Description));
            formData.Add(new StringContent(model.ShortDescription), nameof(BlogAddModel.ShortDescription));
            formData.Add(new StringContent(model.Title), nameof(BlogAddModel.Title));


            //Tokenımızı da istek ile birlikte gönderelim
            //Daha önce token ımızı token olarak 
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));
           await _httpClient.PostAsync("http://localhost:64281/api/blogs/",formData);


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
            var responseMessage = await _httpClient.GetAsync("http://localhost:64281/api/blogs/GetBlogsWithCategories/" + Id);

            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogList>>(await responseMessage.Content.ReadAsStringAsync());
            }

            return null;
        }
    }
}
