using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MovieRetail.Areas.Identity.Data;
using MovieRetail.Models;
using Microsoft.Extensions.Caching.Memory;

namespace MovieRetail.Controllers
{
    public class CommentSectionController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IMemoryCache cache;
        public CommentSectionController(ApplicationDbContext db, IMemoryCache cache)
        {
            this.db = db;
            this.cache = cache;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment(MovieViewModel movieViewModel, int movieId)
        {
            // Implement rate limiting here
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var requestCountKey = $"RequestCount_{userId}";

            // Get the current request count from the cache
            int requestCount = cache.GetOrCreate(requestCountKey, entry =>
            {
                // Set the sliding window duration to 1 minute (adjust as needed)
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                return 0;
            });

            // Define your rate limit (e.g., max 5 comments per 30 minute)
            int maxRateLimit = 5;

            // Check if the user has exceeded the rate limit
            if (requestCount >= maxRateLimit)
            {
                // Return a response indicating rate limit exceeded (e.g., 429 Too Many Requests)
                return StatusCode(429, "Rate limit exceeded. Please try again later.");
            }

            // Increment the request count for the user
            cache.Set(requestCountKey, requestCount + 1);

    
            if (movieViewModel.commentSection.Title == null || movieViewModel.commentSection.Comment == null)
            {
                return NotFound();
            }

            var currentUser = db.Users.Find(userId);
            if (currentUser == null)
                return BadRequest("user not found?");

            movieViewModel.commentSection.UserId = userId;
            movieViewModel.commentSection.FirstName = currentUser.FirstName;
            movieViewModel.commentSection.LastName = currentUser.LastName;
            movieViewModel.commentSection.MovieId = movieId;
            movieViewModel.commentSection.Time = DateTime.Now;

            db.movieComments.Add(movieViewModel.commentSection);
            db.SaveChanges();

            return RedirectToAction("Details", "Movie", new { id = movieId });
        }


        [HttpPost]
        public IActionResult DeleteComment(int id, int movieId)
        {
            var comment = db.movieComments.FirstOrDefault(c =>c.Id==id);
            if (comment == null)
                return NotFound();

            db.movieComments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Details", "Movie", new { id = movieId });
        }
    }
}
