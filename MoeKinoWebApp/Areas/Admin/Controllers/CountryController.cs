using Microsoft.AspNetCore.Mvc;
using MoeKinoWebApp.Data;
using MoeKinoWebApp.Models;

namespace MvcApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CountryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CountryController(ApplicationDbContext db){
            _db = db;
        } 

        public IActionResult Index()
        {
            IEnumerable<Country> objCountryList = _db.Countries;
            return View(objCountryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePOST(Country obj)
        {   
            if (ModelState.IsValid)
            {
                _db.Countries.Add(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0){
                return NotFound();
            }

            var CountryFromDb = _db.Countries.Find(id);

            if (CountryFromDb == null){
                return NotFound();
            }

            return View(CountryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPOST(Country obj)
        {   
            if (ModelState.IsValid)
            {
                _db.Countries.Update(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0){
                return NotFound();
            }

            var CountryFromDb = _db.Countries.Find(id);

            if (CountryFromDb == null){
                return NotFound();
            }

            return View(CountryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int id)
        {   
            var obj = _db.Countries.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Countries.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}