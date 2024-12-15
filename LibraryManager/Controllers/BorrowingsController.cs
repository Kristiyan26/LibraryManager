using LibraryManager.Entities;
using LibraryManager.ExtentionMethods;
using LibraryManager.Repositories;
using LibraryManager.ViewModels.Borrowings;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{
    public class BorrowingsController : Controller
    {
        public IActionResult Index()
        {
            LibraryManagerDbContext context = new LibraryManagerDbContext();

            IndexVM model = new IndexVM();

            Member member = this.HttpContext.Session.GetObject<Member>("loggedMember");

            model.Borrowings = context.Borrowings.Where(x=>x.MemberId==member.MemberId).ToList();
            return View(model);
        }
    }
}
