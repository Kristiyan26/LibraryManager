using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManager.Entities
{
    public class Borrowing
    {

        [Key]
        public int BorrowingId { get; set; }    


        public int BookId {  get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }
   
        public int MemberId { get; set; }

        [ForeignKey("MemberId")]
        public Member Member { get; set; }

        [Required]
        public DateTime BorrowedOn { get; set; }

        public DateTime? ReturnOn { get; set; }


    }
}
