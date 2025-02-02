using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Core.Entities
{
    public class Author : BaseEntity
    {

        public string Name { get; set; }

        public virtual List<BookAuthor> BookAuthors { get; set; }


    }
}
