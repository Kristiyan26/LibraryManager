using LibraryManager.Core.Entities;

namespace LibraryManager.Data.Repositories
{
    public class BookAuthorsRepository : BaseRepository<BookAuthor>
    {
        public BookAuthorsRepository(LibraryManagerDbContext context):base(context)
        {
                
        }
}
}
