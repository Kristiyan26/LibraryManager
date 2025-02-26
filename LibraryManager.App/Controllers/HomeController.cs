using LibraryManager.App.ActionFilters;
using LibraryManager.Core.Entities;
using LibraryManager.App.ExtentionMethods;
using LibraryManager.Data.Repositories;
using LibraryManager.App.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

namespace LibraryManager.App.Controllers
{


    public class HomeController : Controller
    {
        private readonly IPasswordHasher<Member> _passwordHasher;
        public HomeController(IPasswordHasher<Member> passwordHasher)
        {
                _passwordHasher = passwordHasher;   
        }


        [HttpGet]
        [HttpPost]
        public IActionResult Index(string? genre)
        {
            BooksRepository booksRepository = new BooksRepository();
            BookAuthorsRepository bookAuthorsRepository = new BookAuthorsRepository();
            GenresRepository genresRepository = new GenresRepository();


            IndexVM model = new IndexVM();
            model.SelectedGenre = genre;
            model.BookAuthors = bookAuthorsRepository.GetAll();
            model.Genres = genresRepository.GetAll();

            if (!string.IsNullOrEmpty(genre))
            {
                model.Books = booksRepository.GetAll(x => x.Genre.Name == genre);
            }
            else
            {
                model.Books = booksRepository.GetAll();
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Login(LoginVM model)
        {
            MembersRepository membersRepository = new MembersRepository();

            if (!ModelState.IsValid)
            {
                return View(model);
            }



            Member loggedMember = membersRepository.GetFirstOrDefault(x => x.Username == model.Username);

            if (loggedMember == null)
            {
                ModelState.AddModelError("authError", "Invalid username or password!");
                return View(model);
            }
            else
            {
                PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(null, loggedMember.Password, model.Password);
                if (result == PasswordVerificationResult.Failed)
                {
                    ModelState.AddModelError("authError", "Invalid username or password!");
                    return View(model);
                }

            }

            if (loggedMember.Role == "Admin")
            {
                HttpContext.Session.SetObject("loggedAdmin", loggedMember);
                return RedirectToAction("Books", "Admin");
            }
            else
            {
                HttpContext.Session.SetObject("loggedMember", loggedMember);
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpGet]
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetObject<Member>("loggedMember") != null)
            {
                HttpContext.Session.SetObject<Member>("loggedMember", null);
            }
            if (HttpContext.Session.GetObject<Member>("loggedAdmin") != null)
            {
                HttpContext.Session.SetObject<Member>("loggedAdmin", null);
            }

            return RedirectToAction("Index", "Home");
        }


        [AuthenticationFilter]

        public IActionResult Borrow(int id)
        {
            BooksRepository booksRepository = new BooksRepository();
            BorrowingsRepository borrowingsRepository = new BorrowingsRepository();

            Book book = booksRepository.GetFirstOrDefault(b => b.Id == id);

            if (book == null && book.OnStock > 0)
            {
                return RedirectToAction("Index", "Home");
            }


            Member member = HttpContext.Session.GetObject<Member>("loggedMember");



            Borrowing borrowing = new Borrowing();

            borrowing.MemberId = member.Id;
            borrowing.BookId = book.Id;
            borrowing.BorrowedOn = DateTime.Now;

            Borrowing check = borrowingsRepository.GetFirstOrDefault(x => x.MemberId == borrowing.MemberId
                                                                && x.BookId == borrowing.BookId
                                                                && x.ReturnOn == null);


            //old one Borrowing check =
            //context.Borrowings.OrderByDescending(x=>x.BorrowedOn).
            //FirstOrDefault(x => x.MemberId == borrowing.MemberId
            //&& x.BookId == borrowing.BookId);

            if (check != null && check.ReturnOn == null)
            {
                return RedirectToAction("Index", "Home");
            }

            book.OnStock--;
            booksRepository.Save(book);

            borrowingsRepository.Save(borrowing);

            return RedirectToAction("Index", "Borrowings");

        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SignUp(SignUpVM model)
        {
            MembersRepository membersRepository = new MembersRepository();
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            Member check = membersRepository.GetFirstOrDefault(x => x.Username == model.Username);

            if (check != null)
            {
                ModelState.AddModelError("authError", "Username is already taken!");
                return View(model);

            }
            else
            {
                Member newMember = new Member();
                newMember.Username = model.Username;
                newMember.Password = _passwordHasher.HashPassword(null, model.Password);
                newMember.FirstName = model.FirstName;
                newMember.LastName = model.LastName;
                newMember.Role = "Member";

                membersRepository.Save(newMember);
                HttpContext.Session.SetObject("loggedMember", newMember);
            }
            return RedirectToAction("Index", "Home");

        }
    }
}
