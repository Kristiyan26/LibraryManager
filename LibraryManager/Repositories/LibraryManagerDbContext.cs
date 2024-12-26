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
                .UseSqlServer("Server=localhost;Database=LibraryManagerDB;Trusted_Connection=True;TrustServerCertificate=true")
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, Name = "J.K. Rowling" },
                new Author { AuthorId = 2, Name = "George R.R. Martin" },
                new Author { AuthorId = 3, Name = "J.R.R. Tolkien" }
            );

     
            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "Harry Potter and the Philosopher's Stone", GenreId = 1 },
                new Book { BookId = 2, Title = "A Game of Thrones", GenreId = 2 },
                new Book { BookId = 3, Title = "The Hobbit", GenreId = 1 }
            );

      
            modelBuilder.Entity<Genre>().HasData(
                new Genre { GenreId = 1, Name = "Fantasy" },
                new Genre { GenreId = 2, Name = "Epic Fantasy" }
            );

     
            modelBuilder.Entity<BookAuthor>().HasData(
                new BookAuthor { BookId = 1, AuthorId = 1 }, // Harry Potter by J.K. Rowling
                new BookAuthor { BookId = 2, AuthorId = 2 },  // Game of Thrones by George R.R. Martin
                new BookAuthor { BookId = 3, AuthorId = 3 }

            );


         
            modelBuilder.Entity<Member>().HasData(
                 new Member
                 {
                     MemberId = 1,
                     Username = "Admin",
                     Password = "0000",
                     FirstName = "Admin",
                     LastName = "Adminov",
                     Role="Admin"

                 },
                new Member
                {
                    MemberId = 2,
                    Username = "MladMilioner",
                    Password = "0000",
                    FirstName = "Kristiyan",
                    LastName = "Lyubenov",
                    Role="Member"
                    
                },
                new Member 
                { 
                    MemberId = 3,
                    Username="SimonG",
                    Password="0000",
                    FirstName="Stoyan",
                    LastName="Kolev",
                    Role = "Member"
                }
                );

            modelBuilder.Entity<Borrowing>().HasData(
                new Borrowing
                {
                    BorrowingId = 1,
                    MemberId = 2,
                    BookId = 2,
                    BorrowedOn = new DateTime(2024, 12, 26)
                });

        }



    }
}
