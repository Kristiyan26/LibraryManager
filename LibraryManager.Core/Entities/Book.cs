using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManager.Core.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }

        public int GenreId { get; set; }

        public int OnStock { get; set; }


        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }

        public virtual List<Borrowing> Borrowings { get; set; }

        public virtual List<BookAuthor> BookAuthors { get; set; }

        public string ImageUrl { get; set; }
    }
}
