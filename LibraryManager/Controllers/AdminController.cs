using LibraryManager.ActionFilters;
using LibraryManager.Entities;
using LibraryManager.ExtentionMethods;
using LibraryManager.Repositories;
using LibraryManager.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Net;

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
        public IActionResult Edit(int id)
        {
            BooksRepository booksRepository = new BooksRepository();
            BookAuthorsRepository bookAuthorsRepository = new BookAuthorsRepository();
           
            List<Author> authors = new List<Author>();


            EditVM model = new EditVM();

            model.Book = booksRepository.GetFirstOrDefault(x => x.Id == id);

            model.Authors = bookAuthorsRepository.
                GetAll(x => x.BookId == id).Select(x=>x.Author).ToList();   


            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditVM model)
        {
            BooksRepository booksRepository = new BooksRepository();

            Book book = booksRepository.GetFirstOrDefault(x => x.Id == model.Book.Id);

            //$"~/images/{model.ImageUrl}";
            string url = $"~/images/{model.ImageFile.FileName}";
            book.ImageUrl= url;
            book.Title= model.Book.Title;   
            book.OnStock=model.Book.OnStock;

            booksRepository.Save(book);

            return RedirectToAction("Books","Admin");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            BooksRepository booksRepository = new BooksRepository();
            Book book = booksRepository.GetFirstOrDefault( x => x.Id == id);
            booksRepository.Delete(book);
            return RedirectToAction("Books", "Admin");

        }
    }
}
