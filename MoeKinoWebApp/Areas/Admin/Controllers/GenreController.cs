using Microsoft.AspNetCore.Mvc;
using MoeKinoWebApp.Data;
using MoeKinoWebApp.Models;

namespace MvcApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenreController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GenreController(ApplicationDbContext db){
            _db = db;
        } 

        public IActionResult Index()
        {
            IEnumerable<Genre> objGenreList = _db.Genres;
            return View(objGenreList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Genre obj)
        {   
            if (ModelState.IsValid)
            {
                _db.Genres.Add(obj);
                _db.SaveChanges();
                TempData["Success"] = "Genre Created Successfully!";
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0){
                return NotFound();
            }

            var genreFromDb = _db.Genres.Find(id);

            if (genreFromDb == null){
                return NotFound();
            }

            return View(genreFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Genre obj)
        {   
            if (ModelState.IsValid)
            {
                _db.Genres.Update(obj);
                _db.SaveChanges();
                TempData["Success"] = "Genre Updated Successfully!";
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0){
                return NotFound();
            }

            var genreFromDb = _db.Genres.Find(id);

            if (genreFromDb == null){
                return NotFound();
            }

            return View(genreFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {   
            var obj = _db.Genres.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Genres.Remove(obj);
                _db.SaveChanges();
                TempData["Success"] = "Genre Deleted Successfully!";
            }
            return RedirectToAction("Index");
        }
    }
}