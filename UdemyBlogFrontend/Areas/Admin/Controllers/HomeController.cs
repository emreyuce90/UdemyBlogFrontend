using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UdemyBlogFrontend.Filters;

namespace UdemyBlogFrontend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [JwtFilter]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}