using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyBlogFrontend.Models;

namespace UdemyBlogFrontend.ApiServices.Interfaces
{
    public interface IBlogApiService
    {
        Task<List<BlogList>> GetAllAsync();
        Task<List<BlogList>> GetBlogsByCategoryId(int id);
        Task<BlogList> GetBlogDetailByIdAsync(int Id);

        Task<BlogList> AddBlogAsync(BlogList blog);
    }
}
