using LibraryManager.Entities;

namespace LibraryManager.ViewModels.Home
{
    public class IndexVM
    {
        public List<Book> Books { get; set; }
        public List<Author> Authors { get; set; }   

        public List<Genre> Genres { get; set; }
    }
}
