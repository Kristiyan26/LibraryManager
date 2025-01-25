using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Entities
{
    public class Genre : BaseEntity
    {

        public string Name { get; set; }    

        public virtual ICollection<Book> Books { get; set; }
    }
}
