using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UdemyBlogFrontend.CustomExtensions;
using UdemyBlogFrontend.Models;

namespace UdemyBlogFrontend.Filters
{
    public class JwtFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //kontrol edelim token boş mu dolu mu tokenımızın adı token dı
            var token =context.HttpContext.Session.GetString("token");
            if (string.IsNullOrWhiteSpace(token))
            {
                //eğer sessionda token yok ise giriş sayfasına yönlendirme yap
                context.Result = new RedirectToActionResult("SignIn", "Account", new {@area=""});
            }
            else
            {
                //Aktif kullanıcıyı bulmak için backend tarafında aktif kullanıcıyı gösteren metodu çağır
                var httpClient = new HttpClient();
                //istek gönderirken active user authorization gerektirdiği için sessiondaki token ile birlikte gönderiyoruz
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
                var responseMessage=httpClient.GetAsync("http://localhost:64281/api/Auth/ActiveUser").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    //isim soyisim bilgisini al
                    //önce gelen datayı karşılamamız için model olarak isim soyisim propertysi içeren bir model yazalım_ActiveUser.cs
                    //Gelen json datayı kendi modelimize çevirelim
                    var data =JsonConvert.DeserializeObject<ActiveUser>(responseMessage.Content.ReadAsStringAsync().Result);

                    //session sınıfımızı extend edelim
                    //sessionu kullanıcı adısoyadı olarak atama yap
                    context.HttpContext.Session.SetObject("activeUser", data);

                    
                }
                else
                {
                    //redirect
                    context.Result = new RedirectToActionResult("SignIn", "Account",new {@area="" });

                }
            }




        }
    }
}
