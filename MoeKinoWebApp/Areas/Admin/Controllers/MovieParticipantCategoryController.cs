using Microsoft.AspNetCore.Mvc;
using MoeKinoWebApp.Data;
using MoeKinoWebApp.Models;

namespace MvcApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MoviePaticipantCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MoviePaticipantCategoryController(ApplicationDbContext db){
            _db = db;
        } 

        public IActionResult Index()
        {
            IEnumerable<MoviePaticipantCategory> objMoviePaticipantCategoryList = _db.MoviePaticipantCategories;
            return View(objMoviePaticipantCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePOST(MoviePaticipantCategory obj)
        {   
            if (ModelState.IsValid)
            {
                _db.MoviePaticipantCategories.Add(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0){
                return NotFound();
            }

            var MoviePaticipantCategoryFromDb = _db.MoviePaticipantCategories.Find(id);

            if (MoviePaticipantCategoryFromDb == null){
                return NotFound();
            }

            return View(MoviePaticipantCategoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPOST(MoviePaticipantCategory obj)
        {   
            if (ModelState.IsValid)
            {
                _db.MoviePaticipantCategories.Update(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0){
                return NotFound();
            }

            var MoviePaticipantCategoryFromDb = _db.MoviePaticipantCategories.Find(id);

            if (MoviePaticipantCategoryFromDb == null){
                return NotFound();
            }

            return View(MoviePaticipantCategoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int id)
        {   
            var obj = _db.MoviePaticipantCategories.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.MoviePaticipantCategories.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}