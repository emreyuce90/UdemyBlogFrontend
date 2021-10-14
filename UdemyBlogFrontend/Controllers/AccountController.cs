using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UdemyBlogFrontend.ApiServices.Interfaces;
using UdemyBlogFrontend.Models;

namespace UdemyBlogFrontend.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISignInApiService _signInApiService;
        
        public AccountController(ISignInApiService signInApiService)
        {
            _signInApiService = signInApiService;
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            if (await _signInApiService.SignInAsync(model))
            {
                return RedirectToAction("Index","Home",new {@area="Admin"});
            }
            return View();
        }


    }
}