using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyBlogFrontend.ApiServices.Interfaces;

namespace UdemyBlogFrontend.ViewComponents
{
    public class CategoryList:ViewComponent
    {
        private readonly ICategoryApiService _categoryApiservice;
        public CategoryList(ICategoryApiService categoryApiservice)
        {
            _categoryApiservice = categoryApiservice;
        }
        public  IViewComponentResult Invoke()
        {
           
           return View(_categoryApiservice.GetCategoriesWithBlogCount().Result);
        }
    }
}
