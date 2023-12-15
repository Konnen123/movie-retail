using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MovieRetail.Areas.Identity.Data;
using MovieRetail.Models;

namespace MovieRetail.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly SortListOfMovies sortList;

        public FavoriteController(ApplicationDbContext db)
        {
            this.db = db;
            sortList=SortListOfMovies.Instance;
            
        }
        public IActionResult Index(GenreType.GenreType type, SortMovies.SortMovies sort)
        {
            ViewData["Sort"] = (int?)sort;
            ViewData["Dropdown"] = (int?)type;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<FavoriteMovie> favoriteMovies = db.favoriteMovies.Where(movie => movie.UserId == userId).ToList();
            List<Movie> tempList = new List<Movie>();

            foreach (var favoriteMovie in favoriteMovies)
            {
                var currentMovie = db.movies.FirstOrDefault(movie => movie.Id == favoriteMovie.MovieId);
                tempList.Add(currentMovie);
            }

            IEnumerable<Movie> dataForMovies = tempList;
            dataForMovies = sortList.SortMovies(dataForMovies, sort);

            if (type != GenreType.GenreType.All)
            {
                dataForMovies = dataForMovies.Where(movie => movie.Genre == type);
            }

            return View(dataForMovies);
        }
        [HttpGet]
        public IActionResult AddOrRemoveFromFavoriteList(int movieId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favoriteMovie = db.favoriteMovies.FirstOrDefault(movie => movie.UserId == userId && movie.MovieId==movieId);
            if (favoriteMovie == null)
            {
                //we add it to the database
                db.favoriteMovies.Add(new FavoriteMovie(){MovieId = movieId,UserId = userId});
                db.SaveChanges();
               
                return RedirectToAction("Details", "Movie", new { id = movieId });
            }
            //if we get here, we will remove the film from favorite

            db.favoriteMovies.Remove(favoriteMovie);
            db.SaveChanges();
            return RedirectToAction("Details","Movie", new { id = movieId});
        }

   





    }
}
