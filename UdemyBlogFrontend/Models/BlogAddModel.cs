using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyBlogFrontend.Models
{
    public class BlogAddModel
    {
        [Required(ErrorMessage ="Başlık alanı boş geçilemez")]
        [Display(Name ="Başlık :")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Kısa Tanım alanı boş geçilemez")]
        [Display(Name = "Kısa Tanım :")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "İçerik alanı boş geçilemez")]
        [Display(Name = "İçerik :")]
        public string Description { get; set; }

        public IFormFile File { get; set; }

        public int AppUserId { get; set; }
    }
}
