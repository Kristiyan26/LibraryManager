using LibraryManager.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Data
{
    public class LibraryManagerDbContext : DbContext
    {

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public DbSet<Member> Members { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<BookAuthor> BookAuthors { get; set; }

        public DbSet<Borrowing> Borrowings { get; set; }

        public LibraryManagerDbContext()
        {
            Books = Set<Book>();
            Authors = Set<Author>();
            Genres = Set<Genre>();
            Members = Set<Member>();
            Borrowings = Set<Borrowing>();
            BookAuthors = Set<BookAuthor>();

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
                new Author { Id = 1, Name = "J.K. Rowling" },
                new Author { Id = 2, Name = "George R.R. Martin" },
                new Author { Id = 3, Name = "J.R.R. Tolkien" }
            );


            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "Harry Potter and the Philosopher's Stone",
                    GenreId = 1,
                    ImageUrl = "~/images/HarryPotterAndThePhilosopher'sStoneBookCover.jpg",
                    OnStock = 2

                },
                new Book
                {
                    Id = 2,
                    Title = "A Game of Thrones",
                    GenreId = 2,
                    ImageUrl = "~/images/AGameOfThronesBookCover.jpg",
                    OnStock = 0

                },
                new Book
                {
                    Id = 3,
                    Title = "The Hobbit",
                    GenreId = 1,
                    ImageUrl = "~/images/TheHobbitBookCover.jpg",
                    OnStock = 3
                }
            );


            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Fantasy" },
                new Genre { Id = 2, Name = "Epic Fantasy" }
            );


            modelBuilder.Entity<BookAuthor>().HasData(
                new BookAuthor { Id = 1, BookId = 1, AuthorId = 1 }, // Harry Potter by J.K. Rowling
                new BookAuthor { Id = 2, BookId = 2, AuthorId = 2 },  // Game of Thrones by George R.R. Martin
                new BookAuthor { Id = 3, BookId = 3, AuthorId = 3 }

            );



            modelBuilder.Entity<Member>().HasData(
                 new Member
                 {
                     Id = 1,
                     Username = "Admin",
                     Password = "0000",
                     FirstName = "Admin",
                     LastName = "Adminov",
                     Role = "Admin"

                 },
                new Member
                {
                    Id = 2,
                    Username = "MladMilioner",
                    Password = "0000",
                    FirstName = "Kristiyan",
                    LastName = "Lyubenov",
                    Role = "Member"

                },
                new Member
                {
                    Id = 3,
                    Username = "SimonG",
                    Password = "0000",
                    FirstName = "Stoyan",
                    LastName = "Kolev",
                    Role = "Member"
                }
                );

            modelBuilder.Entity<Borrowing>().HasData(
                new Borrowing
                {
                    Id = 1,
                    MemberId = 2,
                    BookId = 2,
                    BorrowedOn = new DateTime(2024, 12, 26)
                });

        }



    }
}
