using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Entities
{

    [PrimaryKey(nameof(BookId), nameof(AuthorId))]
    public class BookAuthor
    {

      //  first part of the composite key

        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

         // second part of the composite key
    
        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
    }
}
