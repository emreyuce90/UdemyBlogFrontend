using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyBlogFrontend.Models;

namespace UdemyBlogFrontend.ApiServices.Interfaces
{
    public interface ICategoryApiService
    {
        Task<List<CategoryListViewModel>> GetAllAsync();
       
        Task<List<CategoryListViewModel>> GetCategoriesWithBlogCount();

        Task<Category> GetCategoryNameByIdAsync(int id);
    }
}
