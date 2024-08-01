using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoeKinoWebApp.Data;
using MoeKinoWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace MvcApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class MovieCountryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MovieCountryController(ApplicationDbContext db){
            _db = db;
        } 

        public IActionResult Index()
        {
            IEnumerable<MovieCountry> objMovieCountryList = _db.MovieCountries;
            return View(objMovieCountryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePOST(MovieCountry obj)
        {   
            _db.MovieCountries.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int movieID, int CountryID)
        {
            if (movieID == 0 || CountryID == 0)
            {
                return NotFound();
            }

            var movieCountryFromDb = _db.MovieCountries
                .FirstOrDefault(mg => mg.MovieID == movieID && mg.CountryID == CountryID);

            if (movieCountryFromDb == null)
            {
                return NotFound();
            }

            return View(movieCountryFromDb);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> DeletePOST(int movieID, int CountryID)
        {

            var obj = await _db.MovieCountries
                .FirstOrDefaultAsync(mg => mg.MovieID == movieID && mg.CountryID == CountryID);

            if (obj == null)
            {
                return NotFound();
            }

            _db.MovieCountries.Remove(obj);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}