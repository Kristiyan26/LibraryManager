using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Entities
{
    public class Member : BaseEntity
    {

        public string Username { get; set; }

        public string Password {  get; set; }   

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }

        public virtual List<Borrowing> Borrowings { get; set; }

    }
}
