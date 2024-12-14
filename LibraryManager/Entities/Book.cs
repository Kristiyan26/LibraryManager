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
        public Genre Genre { get; set; }    

        public List<Borrowing> Borrowings { get; set; }    

        public List<BookAuthor> BookAuthors { get; set; }
    }
}
