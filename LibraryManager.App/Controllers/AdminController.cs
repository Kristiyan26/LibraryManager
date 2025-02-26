using LibraryManager.App.ActionFilters;
using LibraryManager.Core.Entities;
using LibraryManager.App.ViewModels.Admin;
using LibraryManager.App.ExtentionMethods;
using LibraryManager.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Net;

namespace LibraryManager.App.Controllers
{

    [AdminAuthenticationFilter]
    public class AdminController : Controller
    {
        private readonly MembersRepository _membersRepo;
        private readonly BooksRepository _booksRepo;
        private readonly AuthorsRepository _authorsRepo;
        private readonly BookAuthorsRepository _bookAuthorsRepo;
        private readonly GenresRepository _genresRepo;

        public AdminController(MembersRepository membersRepo, BooksRepository booksRepo, AuthorsRepository authorsRepo, BookAuthorsRepository bookAuthorsRepo, GenresRepository genresRepo)
        {
            _membersRepo = membersRepo;
            _booksRepo = booksRepo;
            _authorsRepo = authorsRepo;
            _bookAuthorsRepo = bookAuthorsRepo;
            _genresRepo = genresRepo;
        }
        public IActionResult Members()
        {

            MembersVM model = new MembersVM();

            model.Members = _membersRepo.GetAll(m => m.Role == "Member");

            return View(model);
        }
        public IActionResult Books()
        {

            BooksVM model = new BooksVM();

            model.Books = _booksRepo.GetAll();

            return View(model);
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            AddBookVM model = new AddBookVM();

            model.Authors = _authorsRepo.GetAll();
            model.Genres = _genresRepo.GetAll();

            return View(model);
        }

        [HttpPost]
        public IActionResult AddBook(AddBookVM model)
        {

            Genre genre = null;

            if (model.Genre.Id != 0)
            {
                genre = _genresRepo.GetFirstOrDefault(g => g.Id == model.Genre.Id);
            }
            else
            {
                genre = new Genre();

                genre.Name = model.Genre.Name;

                _genresRepo.Save(genre);

            }
            Book book = new Book();
            book.Title = model.Title;
            book.GenreId = model.Genre.Id;
            book.OnStock = model.Quantity;
            book.ImageUrl = $"~/images/{model.ImageUrl.FileName}";
            _booksRepo.Save(book);



            foreach (Author selectedAuthor in model.SelectedAuthors)
            {
                if (selectedAuthor.Id != 0)
                {
                    BookAuthor bookAuthor = new BookAuthor();
                    bookAuthor.BookId = book.Id;
                    bookAuthor.AuthorId = _authorsRepo.GetFirstOrDefault(a => a.Id == selectedAuthor.Id).Id;
                    _bookAuthorsRepo.Save(bookAuthor);

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

            Author author = new Author();

            author.Name = model.Author.Name;

            _authorsRepo.Save(author);

            return RedirectToAction("AddBook", "Admin");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            List<Author> authors = new List<Author>();


            EditVM model = new EditVM();

            model.Book = _booksRepo.GetFirstOrDefault(x => x.Id == id);

            model.Authors = _bookAuthorsRepo.
                GetAll(x => x.BookId == id).Select(x => x.Author).ToList();


            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditVM model)
        {

            Book book = _booksRepo.GetFirstOrDefault(x => x.Id == model.Book.Id);


            if (model.ImageFile != null)
            {
                string url = $"~/images/{model.ImageFile.FileName}";
                //$"~/images/{model.ImageUrl}";
                book.ImageUrl = url;
            }


            book.Title = model.Book.Title;
            book.OnStock = model.Book.OnStock;

            _booksRepo.Save(book);

            return RedirectToAction("Books", "Admin");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            Book book = _booksRepo.GetFirstOrDefault(x => x.Id == id);
            _booksRepo.Delete(book);
            return RedirectToAction("Books", "Admin");

        }
    }
}
