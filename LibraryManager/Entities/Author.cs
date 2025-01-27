using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Entities
{
    public class Author : BaseEntity
    {

        public string Name { get; set; }

        public virtual List<BookAuthor> BookAuthors { get; set; }    


    }
}
