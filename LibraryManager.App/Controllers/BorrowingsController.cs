using LibraryManager.App.ActionFilters;
using LibraryManager.Core.Entities;
using LibraryManager.App.ExtentionMethods;
using LibraryManager.Data.Repositories;
using LibraryManager.App.ViewModels.Borrowings;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.App.Controllers
{

    [AuthenticationFilter]
    public class BorrowingsController : Controller
    {
        private readonly BorrowingsRepository _borrowingsRepo;
        private readonly BooksRepository _booksRepo;
        public BorrowingsController(BorrowingsRepository borrowingsRepo, BooksRepository booksRepo)
        {
            _borrowingsRepo = borrowingsRepo;
            _booksRepo = booksRepo;
        }
        public IActionResult Index()
        {

            IndexVM model = new IndexVM();

            Member member = HttpContext.Session.GetObject<Member>("loggedMember");

            model.Borrowings = _borrowingsRepo.GetAll(x => x.MemberId == member.Id);
            return View(model);
        }

        public IActionResult ReturnBorrowedBook(int id)
        {

            Borrowing borrowing = _borrowingsRepo.GetFirstOrDefault(b => b.Id == id);
            borrowing.ReturnOn = DateTime.Now;

            Book book = _booksRepo.GetFirstOrDefault(book => book.Id == borrowing.BookId);
            book.OnStock++;

            _booksRepo.Save(book);
            _borrowingsRepo.Save(borrowing);
            return RedirectToAction("Index", "Borrowings");
        }
    }
}
