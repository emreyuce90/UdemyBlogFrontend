using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyBlogFrontend.Models;

namespace UdemyBlogFrontend.ApiServices.Interfaces
{
   public interface ISignInApiService
    {
        Task<bool> SignInAsync(SignInModel signInModel);
    }
}
