using System.ComponentModel.DataAnnotations;
using MovieRetail.Models;
using MovieRetail.SortMovies;

namespace MovieRetail.SortMovies
{
    public enum SortMovies
    {
        [Display(Name = "A-Z")]
        AscendingAlphabetical,
        [Display(Name = "Z-A")]
        DescendingAlphabetical,
        [Display(Name = "Ascending Rating")]
        AscendingRating,
        [Display(Name = "Descending Rating ")]
        DescendingRating,

    }

}

public class SortListOfMovies
{
    private static readonly SortListOfMovies instance = new SortListOfMovies();

    private SortListOfMovies()
    {}

    public static SortListOfMovies Instance => instance;
    public IEnumerable<Movie> SortMovies(IEnumerable<Movie> movieToSort, SortMovies sort)
    {
        switch (sort)
        {
            case MovieRetail.SortMovies.SortMovies.AscendingAlphabetical:
                movieToSort = movieToSort.OrderBy(movie => movie.Title);
                break;
            case MovieRetail.SortMovies.SortMovies.DescendingAlphabetical:
                movieToSort = movieToSort.OrderByDescending(movie => movie.Title);
                break;
            case MovieRetail.SortMovies.SortMovies.AscendingRating:
                movieToSort = movieToSort.OrderBy(movie => movie.Rating);
                break;
            case MovieRetail.SortMovies.SortMovies.DescendingRating:
                movieToSort = movieToSort.OrderByDescending(movie => movie.Rating);
                break;


        }

        return movieToSort;
    }
}