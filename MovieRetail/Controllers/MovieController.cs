using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieRetail.Areas.Identity.Data;
using MovieRetail.Models;
using MovieRetail.SortMovies;


namespace MovieRetail.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly SortListOfMovies sortList;
        
        private readonly int maxImageSize;
        public MovieController(ApplicationDbContext db)
        {
            this.db = db;
            sortList = SortListOfMovies.Instance;
            
            maxImageSize =  1024 * 1024;
        }
        
        
        public IActionResult Index(GenreType.GenreType type, SortMovies.SortMovies sort)
        {
            IEnumerable<Movie> movies = db.movies;
           
            movies = sortList.SortMovies(movies,sort);
            ViewData["Sort"] = (int?)sort;
            ViewData["Dropdown"] = (int?)type;

            if (type != GenreType.GenreType.All)
            {
                movies = movies.Where(movie => movie.Genre == type);
            }

            return View(movies);
        }

        //GET
        [HttpGet]
        [Authorize(Roles="Admin")]
        public IActionResult CreateMovie()
        {
           
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateMovie(Movie movie, IFormFile ImageData)
        {

            if (!ModelState.IsValid)
                return View();

            if (!(ImageData.ContentType == "image/jpeg" || ImageData.ContentType == "image/png"))
            {
                ModelState.AddModelError("ImageData", "Please upload a PNG or JPEG image.");
                return View();
            }
            if (ImageData.Length > maxImageSize) 
            {
                ModelState.AddModelError("ImageData", "The image size must be less than 1 MB.");
                return View(movie);
            }


            movie.ImageData = IFormFileToByteArray(ImageData);
          
            movie.ImageContentType=ImageData.ContentType;
          

            db.movies.Add(movie);
            db.SaveChanges();
            

            return RedirectToAction("Index", "Movie");
        }


        //GET
        [HttpGet]
        public IActionResult Details(int? id)
        {
            ViewData["id"] = id;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);//To check if it is on favorite list
            var favoriteMovie = db.favoriteMovies.FirstOrDefault(movie => movie.UserId == userId && movie.MovieId==id);

            

            if(favoriteMovie != null)
                ViewData["favoriteMovie"] = "red";
            else
            {
                ViewData["favoriteMovie"] = "yellow";
            }

            IEnumerable<Movie> movies = db.movies;
            IEnumerable<MovieComments> comments = db.movieComments.Where(comment=>comment.MovieId==id);
            MovieComments commentSection = new MovieComments();

            MovieViewModel movieViewModel = new MovieViewModel()
            {
                movies = movies,
                allComments = comments,
                commentSection = commentSection
            };
            return View(movieViewModel);
        }
        [HttpGet]
        public IActionResult SearchDetails(string title)
        {
            Movie currentMovie = db.movies.FirstOrDefault(options => options.Title.Contains(title));

            if (currentMovie == null)
                return NotFound();
            return RedirectToAction("Details", new { id = currentMovie.Id });
        }

        //GET
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id < 0)
                return NotFound();
            Movie movie = db.movies.FirstOrDefault(options => options.Id == id);
            if (movie == null)
                return NotFound();
            return View(movie);
        }
        //POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Movie movie,IFormFile ImageData)
        {
            if (!ModelState.IsValid)
                return View();

            if (!(ImageData.ContentType == "image/jpeg" || ImageData.ContentType == "image/png"))
            {
                ModelState.AddModelError("ImageData", "Please upload a PNG or JPEG image.");
                return View();
            }
            if (ImageData.Length > maxImageSize) 
            {
                ModelState.AddModelError("ImageData", "The image size must be less than 1 MB.");
                return View(movie);
            }


            movie.ImageData = IFormFileToByteArray(ImageData);

            movie.ImageContentType = ImageData.ContentType;


            db.movies.Update(movie);
            db.SaveChanges();


            return RedirectToAction("Details", "Movie", new { id = movie.Id });
        }  //GET
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id < 0)
                return NotFound();
            Movie movie = db.movies.FirstOrDefault(options => options.Id == id);
            if (movie == null)
                return NotFound();
            return View(movie);
        }
        //POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Movie movie)
        {
            

            List<FavoriteMovie> favoriteMovies = db.favoriteMovies.Where(favoriteMovie => favoriteMovie.MovieId == movie.Id).ToList();
            db.movies.Remove(movie);

            foreach (var favoriteMovie in favoriteMovies)
            {
                db.favoriteMovies.Remove(favoriteMovie);
            }

            db.SaveChanges();


            return RedirectToAction("Index", "Movie");
        }





        public byte[] IFormFileToByteArray(IFormFile imageIn)
        {
            using (var memoryStream = new MemoryStream())
            {
                imageIn.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
