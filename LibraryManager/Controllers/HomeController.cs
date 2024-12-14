using LibraryManager.Entities;
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

            return View(model);
        }

    }
}
