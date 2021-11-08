using System;

namespace UdemyBlogFrontend.Models
{
    public class CommentAddViewModel
    {
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Description { get; set; }
        public DateTime ReleseDate { get; set; }
        public int? ParentCommentId { get; set; }
        public int BlogId { get; set; }
    }
}