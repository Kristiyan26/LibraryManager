using LibraryManager.Core.Entities;


namespace LibraryManager.Data.Repositories
{
    public class GenresRepository : BaseRepository<Genre>
    {
        public GenresRepository(LibraryManagerDbContext context) : base(context)
        {

        }
    }
}
