using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyBlogFrontend.Models
{
    public class BlogUpdateModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Başlık alanı boş geçilemez")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Açıklama alanı boş geçilemez")]
        public string Description { get; set; }
        [Required(ErrorMessage ="Kısa tanım alanı boş geçilemez")]
        public string ShortDescription { get; set; }
        public IFormFile File { get; set; }
        public int AppUserId { get; set; }


    }
}
