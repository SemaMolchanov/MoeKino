using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoeKinoWebApp.Data;
using MoeKinoWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace MvcApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class MovieParticipantController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MovieParticipantController(ApplicationDbContext db){
            _db = db;
        } 

        public IActionResult Index()
        {
            IEnumerable<MovieParticipant> objMovieParticipantList = _db.MovieParticipants;
            return View(objMovieParticipantList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePOST(MovieParticipant obj)
        {   
            _db.MovieParticipants.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        /*public IActionResult Edit(int movieID, int genreID)
        {
            if (movieID == 0 || genreID == 0)
            {
                return NotFound();
            }

            var MovieParticipantFromDb = _db.MovieParticipants
                .FirstOrDefault(mg => mg.MovieID == movieID && mg.GenreID == genreID);

            if (MovieParticipantFromDb == null)
            {
                return NotFound();
            }

            return View(MovieParticipantFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPOST(MovieParticipant obj)
        {   
            _db.MovieParticipants.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }*/

        public IActionResult Delete(int movieID, int personID, int movieParticipantCategoryID)
        {
            if (movieID == 0 || personID == 0 || movieParticipantCategoryID == 0)
            {
                return NotFound();
            }

            var MovieParticipantFromDb = _db.MovieParticipants
                .FirstOrDefault(mp => mp.MovieID == movieID && mp.PersonID == personID && mp.MovieParticipantCategoryID == movieParticipantCategoryID);

            if (MovieParticipantFromDb == null)
            {
                return NotFound();
            }

            return View(MovieParticipantFromDb);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> DeletePOST(int movieID, int personID, int movieParticipantCategoryID)
        {

            var obj = await _db.MovieParticipants
                .FirstOrDefaultAsync(mp => mp.MovieID == movieID && mp.PersonID == personID && mp.MovieParticipantCategoryID == movieParticipantCategoryID);

            if (obj == null)
            {
                return NotFound();
            }

            _db.MovieParticipants.Remove(obj);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}