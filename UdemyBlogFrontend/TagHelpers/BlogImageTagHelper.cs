using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyBlogFrontend.ApiServices.Interfaces;
using UdemyBlogFrontend.Enums;

namespace UdemyBlogFrontend.TagHelpers
{
    [HtmlTargetElement("getblogimage")]
    public class BlogImageTagHelper : TagHelper
    {
        private readonly IImageApiService _imageApiService;
        public BlogImageTagHelper(IImageApiService imageApiService)
        {
            _imageApiService = imageApiService;
                        
        }
        public BlogImageEnums Enum { get; set; }
        public int Id { get; set; }
        public  override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //Apiden gelen blob değer
            var blob = await _imageApiService.GetBlogImageByIdAsync(Id);

            //image kısmını buraya string olarak yazalım
            var html = string.Empty;

            if (Enum==BlogImageEnums.BlogHome)
            {
                html= $"<img class='card-img-top' src='{blob}'>";
            }
            else
            {
                html = $"<img class='img-fluid rounded' src='{blob}'>";
            }
            
           //html çıktısına html imizi ekleyelim

            output.Content.SetHtmlContent(html);
        }
    }
}
