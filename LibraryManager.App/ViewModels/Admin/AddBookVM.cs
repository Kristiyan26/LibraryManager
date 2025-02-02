using LibraryManager.Core.Entities;

namespace LibraryManager.App.ViewModels.Admin
{
    public class AddBookVM
    {
        public string Title { get; set; }

        public List<Author> SelectedAuthors { get; set; }

        public Genre Genre { get; set; }

        public IFormFile ImageUrl { get; set; }

        public int Quantity { get; set; }

        public List<Genre> Genres { get; set; } //for the view page
        public List<Author> Authors { get; set; } // for the view page
    }
}
