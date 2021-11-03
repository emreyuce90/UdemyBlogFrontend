using System;
using System.Collections.Generic;

namespace UdemyBlogFrontend.Models{
    public class CommentListViewModel{
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }

        public string Description { get; set; }
        public DateTime ReleseDate { get; set; } 

        public int? ParentCommentId { get; set; }
        
        public List<CommentListViewModel> SubComments { get; set; }

        //Bir yorum bloğa yapılabilir
        public int BlogId { get; set; }
        
    }
}