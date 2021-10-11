using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyBlogFrontend.ApiServices.Interfaces;

namespace UdemyBlogFrontend.TagHelpers
{
    [HtmlTargetElement("categoryinfo")]
    public class BlogInfoTagHelper : TagHelper
    {
        private readonly ICategoryApiService _categoryApiService;
        public BlogInfoTagHelper(ICategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }
        public int CategoryId { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {

            var category = await _categoryApiService.GetCategoryNameByIdAsync(CategoryId);

            var html = $"<div class='alert alert-info'> Şu an {category.Name}  adlı kategoriye ait blog yazılarını  görüntülemektesiniz...<a href='Home/Index' class='float-md - right'>Filtreyi kaldır</a></div>";
            output.Content.SetHtmlContent(html);

        }
    }
}
