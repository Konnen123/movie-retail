
using System.ComponentModel.DataAnnotations;
namespace MovieRetail.Models
{
    public class MovieComments
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Text)]
        public string Title { get; set; }
        [Required]
        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        public string UserId { get; set; }
        public int MovieId { get; set; }
        public DateTime Time { get; set; }

    }
}
