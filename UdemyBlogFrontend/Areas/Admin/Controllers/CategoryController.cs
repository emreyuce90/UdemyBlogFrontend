using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UdemyBlogFrontend.ApiServices.Interfaces;

namespace UdemyBlogFrontend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryApiService _categoryService;
        public CategoryController(ICategoryApiService categoryService)
        {
            _categoryService=categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(){
            
            return View(await _categoryService.GetAllCategoriesAsync());
        }
    }
}