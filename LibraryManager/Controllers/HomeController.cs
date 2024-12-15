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
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

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
                this.HttpContext.Session.SetObject("loggedMember", loggedMember);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
