using LibraryManager.Core.Entities;

namespace LibraryManager.Data.Repositories
{
    public class BorrowingsRepository : BaseRepository<Borrowing>
    {
        public BorrowingsRepository(LibraryManagerDbContext context) : base(context)
        {

        }
    }
}
