using LibraryManager.ActionFilters;
using LibraryManager.Entities;
using LibraryManager.ExtentionMethods;
using LibraryManager.Repositories;
using LibraryManager.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{

    [AdminAuthenticationFilter]
    public class AdminController : Controller
    {
        public IActionResult Members()
        {
            LibraryManagerDbContext context = new LibraryManagerDbContext();    

            MembersVM model = new MembersVM();

            model.Members = context.Members.Where(m=>m.Role=="Member").ToList();

            return View(model);
        }
        public IActionResult Books()
        {
			LibraryManagerDbContext context = new LibraryManagerDbContext();

			BooksVM model = new BooksVM();

			model.Books = context.Books.ToList();

			return View(model);
		}

   
    }
}
