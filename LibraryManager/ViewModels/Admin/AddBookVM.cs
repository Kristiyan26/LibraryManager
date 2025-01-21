﻿using LibraryManager.Entities;

namespace LibraryManager.ViewModels.Admin
{
    public class AddBookVM
    {
        public string Title { get; set; }

        public Author Author {  get; set; }

        public Genre Genre {  get; set; }   

        public List<Genre> Genres { get; set; }
        public List<Author> Authors { get; set; }
    }
}
