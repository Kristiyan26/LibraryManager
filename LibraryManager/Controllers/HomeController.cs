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

        public IActionResult Index()
        {
            LibraryManagerDbContext context = new LibraryManagerDbContext();

            IndexVM model = new IndexVM();

            model.Books = context.Books.ToList();

            model.BookAuthors=context.BookAuthors.ToList(); 

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
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }
            
            LibraryManagerDbContext context = new LibraryManagerDbContext();


            Member loggedUser = context.Members.FirstOrDefault(x => x.Username == model.Username &&
                                                                 x.Password == model.Password);

            if(loggedUser == null)
            {
                this.ModelState.AddModelError("authError", "Invalid username or password!");
                return View(model);

            }

            if(loggedUser.Role=="Admin")
            {
                this.HttpContext.Session.SetObject<Member>("loggedAdmin", loggedUser);
				return RedirectToAction("Books", "Admin");
			}
            else 
            {
              

                this.HttpContext.Session.SetObject<Member>("loggedMember", loggedUser);
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
            LibraryManagerDbContext context = new LibraryManagerDbContext();

            Book book = context.Books.FirstOrDefault(b=>b.BookId == id);

            if(book == null)
            {
                return RedirectToAction("Index", "Home");
            }
            

            Member member = this.HttpContext.Session.GetObject<Member>("loggedMember");

           

            Borrowing borrowing = new Borrowing();

            borrowing.MemberId = member.MemberId;
            borrowing.BookId = book.BookId;  
            borrowing.BorrowedOn= DateTime.Now;

            Borrowing check = context.Borrowings.FirstOrDefault(x => x.MemberId == borrowing.MemberId
                                                                && x.BookId == borrowing.BookId);

            if (check != null)
            {
                return RedirectToAction("Index", "Home");
            }



            context.Borrowings.Add(borrowing);
            context.SaveChanges();

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
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }
            LibraryManagerDbContext context = new LibraryManagerDbContext();


            Member check = context.Members.FirstOrDefault(x => x.Username == model.Username);

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

                context.Members.Add(newMember);
                context.SaveChanges();
                this.HttpContext.Session.SetObject<Member>("loggedMember", newMember);
            }
            return RedirectToAction("Index", "Home");

        }
    }
}
