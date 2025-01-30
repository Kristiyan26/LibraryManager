using LibraryManager.ActionFilters;
using LibraryManager.Entities;
using LibraryManager.ExtentionMethods;
using LibraryManager.Repositories;
using LibraryManager.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics; 

namespace LibraryManager.Controllers
{


    public class HomeController : Controller
    {


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
                model.Books = booksRepository.GetAll(x=>x.Genre.Name==genre);
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

            if (!this.ModelState.IsValid)
            {
                return View(model);
            }
          


            Member loggedMember = membersRepository.GetFirstOrDefault(x => x.Username == model.Username &&
                                                                 x.Password == model.Password);

            if(loggedMember == null)
            {
                this.ModelState.AddModelError("authError", "Invalid username or password!");
                return View(model);

            }

            if(loggedMember.Role=="Admin")
            {
                this.HttpContext.Session.SetObject<Member>("loggedAdmin", loggedMember);
				return RedirectToAction("Books", "Admin");
			}
            else 
            {
              

                this.HttpContext.Session.SetObject<Member>("loggedMember", loggedMember);
				return RedirectToAction("Index", "Home");

			}
      
        }

        [HttpGet]
        public IActionResult Logout()
        {
            if(this.HttpContext.Session.GetObject<Member>("loggedMember") != null)
            {
                this.HttpContext.Session.SetObject<Member>("loggedMember", null);
            }
			if (this.HttpContext.Session.GetObject<Member>("loggedAdmin") != null)
			{
				this.HttpContext.Session.SetObject<Member>("loggedAdmin", null);
			}

			return RedirectToAction("Index", "Home");
        }


        [AuthenticationFilter]
   
        public IActionResult Borrow(int id)
        {
            BooksRepository booksRepository = new BooksRepository();
            BorrowingsRepository borrowingsRepository = new BorrowingsRepository();

            Book book = booksRepository.GetFirstOrDefault(b=>b.Id == id);

            if(book == null && book.OnStock>0)
            {
                return RedirectToAction("Index", "Home");
            }
            

            Member member = this.HttpContext.Session.GetObject<Member>("loggedMember");

           

            Borrowing borrowing = new Borrowing();

            borrowing.MemberId = member.Id;
            borrowing.BookId = book.Id;  
            borrowing.BorrowedOn= DateTime.Now;

            Borrowing check = borrowingsRepository.GetFirstOrDefault(x => x.MemberId == borrowing.MemberId
                                                                && x.BookId == borrowing.BookId 
                                                                && x.ReturnOn==null);


            //old one Borrowing check =
            //context.Borrowings.OrderByDescending(x=>x.BorrowedOn).
            //FirstOrDefault(x => x.MemberId == borrowing.MemberId
            //&& x.BookId == borrowing.BookId);

            if (check!=null && check.ReturnOn == null)
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
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }


            Member check = membersRepository.GetFirstOrDefault(x => x.Username == model.Username);

            if (check != null)
            {
                this.ModelState.AddModelError("authError", "Username is already taken!");
                return View(model);

            }
            else
            {
                Member newMember = new Member();
                newMember.Username = model.Username;
                newMember.Password = model.Password;
                newMember.FirstName= model.FirstName;   
                newMember.LastName= model.LastName;

                membersRepository.Save(newMember);
                this.HttpContext.Session.SetObject<Member>("loggedMember", newMember);
            }
            return RedirectToAction("Index", "Home");

        }
    }
}
