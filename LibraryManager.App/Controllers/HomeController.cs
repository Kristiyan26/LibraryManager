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
        private readonly MembersRepository _membersRepo;
        private readonly BooksRepository _booksRepo;
        private readonly BookAuthorsRepository _bookAuthorsRepo;
        private readonly GenresRepository _genresRepo;
        private readonly BorrowingsRepository _borrowingsRepo;

        public HomeController(IPasswordHasher<Member> passwordHasher, MembersRepository membersRepo, BooksRepository booksRepo,BookAuthorsRepository bookAuthorsRepo, GenresRepository genresRepo, BorrowingsRepository borrowingsRepo)
        {
            _passwordHasher = passwordHasher;
            _membersRepo = membersRepo;
            _booksRepo = booksRepo;
            _bookAuthorsRepo = bookAuthorsRepo;
            _genresRepo = genresRepo;
            _borrowingsRepo = borrowingsRepo;
        }


        [HttpGet]
        [HttpPost]
        public IActionResult Index(string? genre)
        {

            IndexVM model = new IndexVM();
            model.SelectedGenre = genre;
            model.BookAuthors = _bookAuthorsRepo.GetAll();
            model.Genres = _genresRepo.GetAll();

            if (!string.IsNullOrEmpty(genre))
            {
                model.Books = _booksRepo.GetAll(x => x.Genre.Name == genre);
            }
            else
            {
                model.Books = _booksRepo.GetAll();
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

            if (!ModelState.IsValid)
            {
                return View(model);
            }



            Member loggedMember = _membersRepo.GetFirstOrDefault(x => x.Username == model.Username);

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


            Book book = _booksRepo.GetFirstOrDefault(b => b.Id == id);

            if (book == null && book.OnStock > 0)
            {
                return RedirectToAction("Index", "Home");
            }


            Member member = HttpContext.Session.GetObject<Member>("loggedMember");



            Borrowing borrowing = new Borrowing();

            borrowing.MemberId = member.Id;
            borrowing.BookId = book.Id;
            borrowing.BorrowedOn = DateTime.Now;

            Borrowing check = _borrowingsRepo.GetFirstOrDefault(x => x.MemberId == borrowing.MemberId
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
            _booksRepo.Save(book);

            _borrowingsRepo.Save(borrowing);

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
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            Member check = _membersRepo.GetFirstOrDefault(x => x.Username == model.Username);

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

                _membersRepo.Save(newMember);
                HttpContext.Session.SetObject("loggedMember", newMember);
            }
            return RedirectToAction("Index", "Home");

        }
    }
}
