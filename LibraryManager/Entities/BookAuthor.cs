using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Entities
{


    public class BookAuthor : BaseEntity
    {

        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }
    }
}
