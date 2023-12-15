using MovieRetail.Models;

namespace MovieRetail
{
    public class MovieViewModel
    {
        public IEnumerable<Movie> movies { get; set; }
        public IEnumerable<MovieComments> allComments { get; set; }
        public MovieComments commentSection { get; set; }
    }

    public class MovieHomeViewModel
    {
        public IEnumerable<Movie> movie { get; set; }
        public IEnumerable<FavoriteMovie> FavoriteMovies { get; set; }
    }
}
