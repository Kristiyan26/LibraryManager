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


            Member loggedMember = context.Members.FirstOrDefault(x => x.Username == model.Username &&
                                                                 x.Password == model.Password);

            if(loggedMember == null)
            {
                this.ModelState.AddModelError("authError", "Invalid username or password!");
                return View(model);

            }
            else
            {
                this.HttpContext.Session.SetObject<Member>("loggedMember", loggedMember);
            }
            return RedirectToAction("Index", "Home");
        }


        [AuthenticationFilter]
        [HttpGet]
        public IActionResult Logout()
        {
            this.HttpContext.Session.SetObject<Member>("loggedMember",null);
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

            //TODO CHECK FOR A BORROWING THAT HAS ALREADY BEEN ADDED



            context.Borrowings.Add(borrowing);
            context.SaveChanges();

            return RedirectToAction("Index", "Borrowings");


        }
    }
}
