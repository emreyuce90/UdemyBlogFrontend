using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyBlogFrontend.CustomExtensions
{
    public static class CustomSessionExtension
    {
        //Value değerini JSON biçimine dönüştür ve parametre olarak verilen key değeri ile birlikte session a at
        public static void SetObject(this ISession session, string key, object value)
        {
            //Session sınıfını genişlettik
            //obje olarak gelen value yu json şekline dönüştür
            var jsonData = JsonConvert.SerializeObject(value);
            //sana gelen key ile value yu session a at
            session.SetString(key, jsonData);
        }


        //gönderilen key değerinin sessionda olup olmadığını kontrol et
        //key değeri var ise bu değeri stringe dönüştür
        public static T GetObject<T>(this ISession session, string key) where T : class, new()
        {
            //sessiondaki key değerini oku
            var data = session.GetString(key);
            if (String.IsNullOrWhiteSpace(data))
            {
                //eğer key değeri boşsa
                return null;
            }
            //eğer key kısmı dolu ise bu değeri sana verdiğim modele deserilize et
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}



