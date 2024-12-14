using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManager.Entities
{
    public class Book
    {
        [Key]
        public int BookId {  get; set; }

        public string Title { get; set; }   

        public int GenreId { get; set; }


        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }    

        public virtual List<Borrowing> Borrowings { get; set; }    

        public virtual List<BookAuthor> BookAuthors { get; set; }
    }
}
