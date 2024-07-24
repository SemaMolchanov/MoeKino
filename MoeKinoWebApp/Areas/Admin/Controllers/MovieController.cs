using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MoeKinoWebApp.Data;
using MoeKinoWebApp.Models;

namespace MvcApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MovieController(ApplicationDbContext db){
            _db = db;
        } 

        public IActionResult Index()
        {
            IEnumerable<Movie> objMovieList = _db.Movies;
            return View(objMovieList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePOST(Movie obj)
        {   
            ModelState.Remove(nameof(obj.TrailerLinkEn));
            ModelState.Remove(nameof(obj.TrailerLinkRu));

            if (ModelState.IsValid)
            {
                _db.Movies.Add(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0){
                return NotFound();
            }

            var MovieFromDb = _db.Movies.Find(id);

            if (MovieFromDb == null){
                return NotFound();
            }

            return View(MovieFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPOST(Movie obj)
        {   
            ModelState.Remove(nameof(obj.TrailerLinkEn));
            ModelState.Remove(nameof(obj.TrailerLinkRu));

            if (ModelState.IsValid){
                _db.Movies.Update(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0){
                return NotFound();
            }

            var MovieFromDb = _db.Movies.Find(id);

            if (MovieFromDb == null){
                return NotFound();
            }

            return View(MovieFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int id)
        {   
            var obj = _db.Movies.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Movies.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}