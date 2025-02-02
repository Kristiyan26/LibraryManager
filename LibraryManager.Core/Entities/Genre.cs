using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Core.Entities
{
    public class Genre : BaseEntity
    {

        public string Name { get; set; }

        public virtual List<Book> Books { get; set; }
    }
}
