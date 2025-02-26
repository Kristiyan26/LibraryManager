using LibraryManager.Core.Entities;


namespace LibraryManager.Data.Repositories
{
    public class MembersRepository : BaseRepository<Member>
    {
        public MembersRepository(LibraryManagerDbContext context) : base(context)
        {

        }
    }
}
