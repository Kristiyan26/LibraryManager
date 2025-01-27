using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryManager.Entities
{
    public class Borrowing : BaseEntity
    {  


        public int BookId {  get; set; }

        [ForeignKey("BookId")]
        [JsonIgnore]
        public virtual Book Book { get; set; }
   
        public int MemberId { get; set; }

        [ForeignKey("MemberId")]
        [JsonIgnore]
        public virtual Member Member { get; set; }

        [Required]
        public DateTime BorrowedOn { get; set; }

        public DateTime? ReturnOn { get; set; }



    }
}
