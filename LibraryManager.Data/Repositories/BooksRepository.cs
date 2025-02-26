using LibraryManager.Core.Entities;

namespace LibraryManager.Data.Repositories
{
    public class BooksRepository : BaseRepository<Book>
    {
        public BooksRepository(LibraryManagerDbContext context) : base(context)
        {

        }
    }
}
