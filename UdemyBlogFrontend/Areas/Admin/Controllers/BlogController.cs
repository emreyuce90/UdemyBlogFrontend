using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UdemyBlogFrontend.ApiServices.Interfaces;
using UdemyBlogFrontend.Filters;
using UdemyBlogFrontend.Models;

namespace UdemyBlogFrontend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [JwtFilter]
    public class BlogController : Controller
    {
        private readonly IBlogApiService _blogApiService;
        public BlogController(IBlogApiService blogApiService)
        {
            _blogApiService = blogApiService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _blogApiService.GetAllAsync());
        }


        [HttpGet]
        public async Task<IActionResult> AddBlog()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddBlog(BlogList blog)
        {
            await _blogApiService.AddBlogAsync(blog);

            return RedirectToAction("Index", null);
        }


    }
}