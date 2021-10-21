using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UdemyBlogFrontend.ApiServices.Interfaces;
using UdemyBlogFrontend.Models;

namespace UdemyBlogFrontend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryApiService _categoryService;
        public CategoryController(ICategoryApiService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            return View(await _categoryService.GetAllCategoriesAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            return View(await _categoryService.GetCategoryByCategoryIdAsync(id));
        }


        [HttpPost]
        public async Task<IActionResult> Update(CategoryList model)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategoryAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Create()
        {

            return View(new CategoryList());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryList category)
        {
            if(ModelState.IsValid){

                await _categoryService.CreateCategoryAsync(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }



    }
}