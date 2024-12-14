using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Entities
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        public string Username { get; set; }

        public string Password {  get; set; }   

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}
