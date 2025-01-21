using LibraryManager.ActionFilters;
using LibraryManager.Entities;
using LibraryManager.ExtentionMethods;
using LibraryManager.Repositories;
using LibraryManager.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace LibraryManager.Controllers
{

    [AdminAuthenticationFilter]
    public class AdminController : Controller
    {
        public IActionResult Members()
        {
            LibraryManagerDbContext context = new LibraryManagerDbContext();

            MembersVM model = new MembersVM();

            model.Members = context.Members.Where(m => m.Role == "Member").ToList();

            return View(model);
        }
        public IActionResult Books()
        {

            LibraryManagerDbContext context = new LibraryManagerDbContext();

            BooksVM model = new BooksVM();

            model.Books = context.Books.ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            AddBookVM model = new AddBookVM();
            LibraryManagerDbContext context = new LibraryManagerDbContext();

            model.Authors = context.Authors.ToList();
            model.Genres = context.Genres.ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult AddBook(AddBookVM model)
        {
            LibraryManagerDbContext context = new LibraryManagerDbContext();
            Genre genre = null;
            Author author = null;

            if (model.Genre.GenreId != 0)
            {
                genre = context.Genres.FirstOrDefault(g => g.GenreId == model.Genre.GenreId);
            }
            else
            {
                genre = new Genre();

                genre.Name = model.Genre.Name;

                context.Genres.Add(genre);
                context.SaveChanges();


            }

            if (model.Author.AuthorId != 0)
            {
                author = context.Authors.FirstOrDefault(a => a.AuthorId == model.Author.AuthorId);


            }
            else
            {
                author = new Author();
                author.Name = model.Author.Name;

                context.Authors.Add(author);
                context.SaveChanges();
            }

            Book book = new Book();
            book.Title = model.Title;
            book.GenreId = genre.GenreId;
            context.Books.Add(book);
            context.SaveChanges();

            BookAuthor bookAuthor = new BookAuthor();
            bookAuthor.AuthorId = author.AuthorId;
            bookAuthor.BookId = book.BookId;
            context.BookAuthors.Add(bookAuthor);
            context.SaveChanges();


            return RedirectToAction("Books", "Admin");
        }

    }
}
