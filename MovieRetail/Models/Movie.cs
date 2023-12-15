
using System.ComponentModel.DataAnnotations;

namespace MovieRetail.Models
{
    public class Movie
    {
        
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; } 
        [Required]
        [StringLength(250)]
        public string Info { get; set; }

        [Required]
        public GenreType.GenreType Genre { get; set; }
        [Required]
        [Range(0,10)]
        public float Rating { get; set; }


        public byte[]? ImageData { get; set; } // Byte array to store the image data
        public string? ImageContentType { get; set; } // Mime type of the image (e.g., image/jpeg, image/png)

        [Required]
        [StringLength(100)]
        [Url]
        public string VideoLink { get; set; } // Byte array to store the video data


    }
}
