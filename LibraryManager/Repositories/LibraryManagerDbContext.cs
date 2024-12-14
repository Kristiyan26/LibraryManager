using LibraryManager.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Repositories
{
    public class LibraryManagerDbContext : DbContext
    {

        public DbSet<Book>Books { get; set; }
        public DbSet<Author> Authors {  get; set; }

        public DbSet<Member> Members { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<BookAuthor> BookAuthors { get; set; }

        public DbSet<Borrowing> Borrowings { get; set; }

        public LibraryManagerDbContext()
        {
            this.Books = this.Set<Book>();
            this.Authors=this.Set<Author>();
            this.Genres = this.Set<Genre>();
            this.Members=this.Set<Member>();
            this.Borrowings=this.Set<Borrowing>();
            this.BookAuthors=this.Set<BookAuthor>();
      
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=localhost;Database=LibraryManagerDB;Trusted_Connection=True;TrustServerCertificate=true");
        }



    }
}
