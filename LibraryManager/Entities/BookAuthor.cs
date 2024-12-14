using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Entities
{
    public class BookAuthor
    {

        [Key, Column(Order = 0)] //  first part of the composite key

        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

        [Key, Column(Order = 1)] // second part of the composite key
    
        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
    }
}
