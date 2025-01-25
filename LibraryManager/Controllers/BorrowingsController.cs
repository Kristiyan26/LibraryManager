using LibraryManager.ActionFilters;
using LibraryManager.Entities;
using LibraryManager.ExtentionMethods;
using LibraryManager.Repositories;
using LibraryManager.ViewModels.Borrowings;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{

    [AuthenticationFilter]
    public class BorrowingsController : Controller
    {
        public IActionResult Index()
        {
            BorrowingsRepository borrowingsRepository = new BorrowingsRepository();

            IndexVM model = new IndexVM();

            Member member = this.HttpContext.Session.GetObject<Member>("loggedMember");

            model.Borrowings = borrowingsRepository.GetAll(x => x.MemberId == member.Id);
            return View(model);
        }

        public IActionResult ReturnBorrowedBook(int id)
        {
            BorrowingsRepository borrowingsRepository = new BorrowingsRepository();
            BooksRepository booksRepository = new BooksRepository();    

            Borrowing borrowing = borrowingsRepository.GetFirstOrDefault(b => b.Id == id);
            borrowing.ReturnOn  = DateTime.Now;

            Book book = booksRepository.GetFirstOrDefault(book => book.Id == borrowing.BookId);
            book.OnStock++;

            booksRepository.Save(book);
            borrowingsRepository.Save(borrowing);
            return RedirectToAction("Index","Borrowings");
        }
    }
}
