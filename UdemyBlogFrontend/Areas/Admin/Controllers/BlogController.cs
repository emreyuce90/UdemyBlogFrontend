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
        public IActionResult AddBlog()
        {
            return View(new BlogAddModel());
        }


        [HttpPost]
        public async Task<IActionResult> AddBlog(BlogAddModel blog)
        {
            if (ModelState.IsValid)
            {
                await _blogApiService.AddBlogAsync(blog);
                return RedirectToAction("Index");
            }

            return View(blog);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBlog(int id)
        {

            return View(await _blogApiService.GetBlogByBlogIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBlog(int id, BlogUpdateModel model)
        {
            if (ModelState.IsValid)
            {

                await _blogApiService.UpdateBlogAsync(model.Id, model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        
        public async Task<IActionResult> DeleteBlog(int id)
        {
            await _blogApiService.DeleteBlogAsync(id);
            return RedirectToAction("Index");
        }


    }
}