using LibraryManager.Entities;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.ViewModels.Admin
{
    public class EditVM
    {
        public Book Book { get; set; }
        [Display(Name = "New Image")]
        public IFormFile ImageFile { get; set; }
        public List<Author> Authors { get; set; }
    }
}
