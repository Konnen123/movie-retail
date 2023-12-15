using System.ComponentModel.DataAnnotations;

namespace MovieRetail.Models
{
    public class FavoriteMovie
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
    }
}
