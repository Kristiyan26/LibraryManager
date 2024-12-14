﻿using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Entities
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        public string Name { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }    


    }
}
