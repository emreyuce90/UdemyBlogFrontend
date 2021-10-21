using System.ComponentModel.DataAnnotations;

namespace UdemyBlogFrontend.Models{
    public class CategoryList{
        public int Id { get; set; }
        [Required(ErrorMessage ="Kategori adı alanı boş geçilemez")]
        public string Name { get; set; }
    }
}