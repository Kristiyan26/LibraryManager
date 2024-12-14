using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Entities
{
    public class Genre
    {

        [Key]
        public int GenreId { get; set; }

        public string Name { get; set; }    

        public ICollection<Book> Books { get; set; }
    }
}
