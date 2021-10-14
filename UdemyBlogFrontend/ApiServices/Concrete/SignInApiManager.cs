using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UdemyBlogFrontend.ApiServices.Interfaces;
using UdemyBlogFrontend.Models;


namespace UdemyBlogFrontend.ApiServices.Concrete
{
    public class SignInApiManager : ISignInApiService
    {
        private readonly HttpClient _httpclient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SignInApiManager(HttpClient httpClient,IHttpContextAccessor httpContextAccessor)
        {
            
            _httpclient = httpClient;
            _httpclient.BaseAddress = new Uri("http://localhost:64281/api/auth/");
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> SignInAsync(SignInModel signInModel)
        {
            //signın modeli jsona çevirdik
            var json = JsonConvert.SerializeObject(signInModel);
            //stringContent oluşturduk
            StringContent stringContent = new StringContent(json, encoding: Encoding.UTF8, "application/json");
            //post işlemi
            var responseMessage = await _httpclient.PostAsync("SignIn", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                //eğer post işlemi başarılı olur ise backend tarafında bir token oluşacak ve bu tokenı bizim karşılamamız lazım.
                //json şeklinde gelen token ı modelimize çevirdik
                var tokenHandler = JsonConvert.DeserializeObject<TokenHandler>(await responseMessage.Content.ReadAsStringAsync());
                //şimdi bu tokenı sessiona atayalım
                //httpcontextaccessor sınıfını ctor da geçip di aracılığıyla enjekte edelim
                _httpContextAccessor.HttpContext.Session.SetString("token", tokenHandler.Token);
                //artık true dönebiliriz tokenımızı ele aldık
                return true;
            }

            return false;

        }
    }
}
