using Microsoft.AspNetCore.Mvc;
using MoeKinoWebApp.Data;
using MoeKinoWebApp.Models;

namespace MvcApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieParticipantCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MovieParticipantCategoryController(ApplicationDbContext db){
            _db = db;
        } 

        public IActionResult Index()
        {
            IEnumerable<MovieParticipantCategory> objMovieParticipantCategoryList = _db.MovieParticipantCategories;
            return View(objMovieParticipantCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePOST(MovieParticipantCategory obj)
        {   
            if (ModelState.IsValid)
            {
                _db.MovieParticipantCategories.Add(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0){
                return NotFound();
            }

            var MovieParticipantCategoryFromDb = _db.MovieParticipantCategories.Find(id);

            if (MovieParticipantCategoryFromDb == null){
                return NotFound();
            }

            return View(MovieParticipantCategoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPOST(MovieParticipantCategory obj)
        {   
            if (ModelState.IsValid)
            {
                _db.MovieParticipantCategories.Update(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0){
                return NotFound();
            }

            var MovieParticipantCategoryFromDb = _db.MovieParticipantCategories.Find(id);

            if (MovieParticipantCategoryFromDb == null){
                return NotFound();
            }

            return View(MovieParticipantCategoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int id)
        {   
            var obj = _db.MovieParticipantCategories.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.MovieParticipantCategories.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}