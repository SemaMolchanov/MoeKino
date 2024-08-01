using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoeKinoWebApp.Data;
using MoeKinoWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace MvcApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class MovieGenreController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MovieGenreController(ApplicationDbContext db){
            _db = db;
        } 

        public IActionResult Index()
        {
            IEnumerable<MovieGenre> objMovieGenreList = _db.MovieGenres;
            return View(objMovieGenreList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePOST(MovieGenre obj)
        {   
            _db.MovieGenres.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int movieID, int genreID)
        {
            if (movieID == 0 || genreID == 0)
            {
                return NotFound();
            }

            var movieGenreFromDb = _db.MovieGenres
                .FirstOrDefault(mg => mg.MovieID == movieID && mg.GenreID == genreID);

            if (movieGenreFromDb == null)
            {
                return NotFound();
            }

            return View(movieGenreFromDb);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> DeletePOST(int movieID, int genreID)
        {

            var obj = await _db.MovieGenres
                .FirstOrDefaultAsync(mg => mg.MovieID == movieID && mg.GenreID == genreID);

            if (obj == null)
            {
                return NotFound();
            }

            _db.MovieGenres.Remove(obj);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}