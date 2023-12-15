using Microsoft.AspNetCore.Mvc;
using MovieRetail.Models;
using System.Diagnostics;
using System.Security.Claims;
using MovieRetail.Areas.Identity.Data;

namespace MovieRetail.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            this.db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MovieHomeViewModel movieHomeViewModel = new MovieHomeViewModel()
            {
                FavoriteMovies = db.favoriteMovies.Where(movie=>movie.UserId==userId),
                movie = db.movies
            };
            return View(movieHomeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}