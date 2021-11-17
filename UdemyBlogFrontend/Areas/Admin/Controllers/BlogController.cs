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

        [HttpGet]
        public async Task<IActionResult> AssignCategory(int id, [FromServices] ICategoryApiService _categoryService)
        {
            TempData["BlogId"] = id;
            //Tüm kategorileri çek
            var categories = await _categoryService.GetAllAsync();
            //BlogId ile gelen bloğun kategorilerini çek
            var bCategory = (await _blogApiService.GetCategoriesByBlogIdAsync(id)).Select(I => I.Name).ToList();
            //Kategorileri listele bloğa eklenenleri işaretlenmiş olarak göster
            List<AssignCategoryViewModel> model = new List<AssignCategoryViewModel>();

            //Tüm kategorileri dön
            foreach (var category in categories)
            {
                AssignCategoryViewModel c = new AssignCategoryViewModel();
                c.CategoryId = category.Id;
                c.CategoryName = category.Name;
                //Eğer blogKategori adı içeriyorsa gelen kategorini adını true döner 
                c.Exists = bCategory.Contains(category.Name);

                model.Add(c);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignCategory(List<AssignCategoryViewModel> model)
        {
            //Liste olarak AssignCategory gelecek
            int BlogId = (int)TempData["BlogId"];

            foreach (var category in model)
            {
                //her bir itemi dönelim ve existi true olanları ekleyelim
                if (category.Exists)
                {
                    //kategoriyi bloğa ekle
                    AddCategory c = new AddCategory();
                    c.CategoryId = category.CategoryId;
                    c.BlogId = BlogId;
                    await _blogApiService.AddCategoryToBlogsAsync(c);
                }
                else
                {
                    AddCategory c = new AddCategory();
                    c.BlogId = BlogId;
                    c.CategoryId = category.CategoryId;

                    await _blogApiService.DeleteCategoryFromBlog(c);
                }
            }
            return RedirectToAction("Index");
        }
    }
}