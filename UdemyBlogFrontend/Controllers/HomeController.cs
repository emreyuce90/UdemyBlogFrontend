using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UdemyBlogFrontend.ApiServices.Interfaces;
using UdemyBlogFrontend.Models;

namespace UdemyBlogFrontend.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IBlogApiService _blogApiService;
        private readonly ICategoryApiService _categoryApiService;
        public HomeController(IBlogApiService blogApiService, ICategoryApiService categoryApiService)
        {
            _blogApiService = blogApiService;
            _categoryApiService = categoryApiService;
           
        }
        public async Task<IActionResult> Index(int? categoryId)
        {
            
            if (categoryId.HasValue)
            {
              
                ViewBag.ActiveCategory = categoryId;
                return View(await _blogApiService.GetBlogsByCategoryId((int)categoryId));
               
            }
            return View(await _blogApiService.GetAllAsync());
        }

        public async Task<IActionResult> BlogDetail(int id)
        {
            //Yorumları çek
            ViewBag.Comments=await _blogApiService.GetCommentsAsync(id,null);
            return View(await _blogApiService.GetBlogDetailByIdAsync(id));
        }


    }
}