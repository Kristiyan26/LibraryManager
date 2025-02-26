using LibraryManager.Core.Entities;
namespace LibraryManager.Data.Repositories
{
    public class AuthorsRepository : BaseRepository<Author>
    {
        public AuthorsRepository(LibraryManagerDbContext context):base(context)
        {
                
        }
    }
}
