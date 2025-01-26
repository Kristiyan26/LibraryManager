using LibraryManager.Entities;

namespace LibraryManager.ViewModels.Admin
{
    public class EditVM
    {
        public Book Book { get; set; }
        public List<Author> Authors { get; set; }
    }
}
