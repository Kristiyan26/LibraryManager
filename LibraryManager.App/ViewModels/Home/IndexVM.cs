using LibraryManager.Core.Entities;

namespace LibraryManager.App.ViewModels.Home
{
    public class IndexVM
    {
        public List<Book> Books { get; set; }

        public List<Genre> Genres { get; set; }

        public string SelectedGenre { get; set; }

        public List<BookAuthor> BookAuthors { get; set; }




    }
}
