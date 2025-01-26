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
            MembersRepository membersRepository = new MembersRepository();

            MembersVM model = new MembersVM();

            model.Members = membersRepository.GetAll(m => m.Role == "Member");

            return View(model);
        }
        public IActionResult Books()
        {
            BooksRepository booksRepository = new BooksRepository();

            BooksVM model = new BooksVM();

            model.Books = booksRepository.GetAll();

            return View(model);
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            AddBookVM model = new AddBookVM();

            AuthorsRepository authorsRepository = new AuthorsRepository();
            GenresRepository genresRepository = new GenresRepository();

            model.Authors = authorsRepository.GetAll();
            model.Genres = genresRepository.GetAll();

            return View(model);
        }

        [HttpPost]
        public IActionResult AddBook(AddBookVM model)
        {



            GenresRepository genresRepository = new GenresRepository();
            AuthorsRepository authorsRepository = new AuthorsRepository();
            BooksRepository booksRepository = new BooksRepository();
            BookAuthorsRepository bookAuthorsRepository = new BookAuthorsRepository();


            Genre genre = null;

            if (model.Genre.Id != 0)
            {
                genre = genresRepository.GetFirstOrDefault(g => g.Id == model.Genre.Id);
            }
            else
            {
                genre = new Genre();

                genre.Name = model.Genre.Name;

                genresRepository.Save(genre);

            }
            Book book = new Book();
            book.Title = model.Title;
            book.GenreId = model.Genre.Id;
            book.OnStock = model.Quantity;
            book.ImageUrl = $"~/images/{model.ImageUrl}";
            booksRepository.Save(book);



            foreach (Author selectedAuthor in model.SelectedAuthors)
            {
                if (selectedAuthor.Id != 0)
                {
                    BookAuthor bookAuthor = new BookAuthor();
                    bookAuthor.BookId = book.Id;
                    bookAuthor.AuthorId = authorsRepository.GetFirstOrDefault(a => a.Id == selectedAuthor.Id).Id;
                    bookAuthorsRepository.Save(bookAuthor);

                }
        
            }

            return RedirectToAction("Books", "Admin");
        }

        [HttpGet]
        public IActionResult AddAuthor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAuthor(AddAuthorVM model)
        {
            AuthorsRepository authorsRepository = new AuthorsRepository();

            Author author = new Author();

            author.Name = model.Author.Name;

            authorsRepository.Save(author);


            return RedirectToAction("AddBook", "Admin");
        }

        [HttpGet]
        public IActionResult EditAuthor(int id)
        {
            BooksRepository booksRepository = new BooksRepository();
            BookAuthorsRepository bookAuthorsRepository = new BookAuthorsRepository();

            EditVM model = new EditVM();

            model.Book = booksRepository.GetFirstOrDefault(x=>x.Id==id);

            model.Authors= bookAuthorsRepository.GetAll(x=>x.BookId==)

            return View();
        }

    }
}
