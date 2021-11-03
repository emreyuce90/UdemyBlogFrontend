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
        Task AddBlogAsync(BlogAddModel model);

        Task<BlogUpdateModel> GetBlogByBlogIdAsync(int id);

        Task UpdateBlogAsync(int id,BlogUpdateModel model);

        Task DeleteBlogAsync(int id);

        Task<List<CommentListViewModel>> GetCommentsAsync(int BlogId,int? parentId);

        
    }
}
