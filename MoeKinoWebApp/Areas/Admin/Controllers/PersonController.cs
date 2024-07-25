using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MoeKinoWebApp.Data;
using MoeKinoWebApp.Models;

namespace MvcApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PersonController(ApplicationDbContext db){
            _db = db;
        } 

        public IActionResult Index()
        {
            IEnumerable<Person> objPersonList = _db.Persons;
            return View(objPersonList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePOST(Person obj)
        {   
            ModelState.Remove(nameof(obj.ShortBioEn));
            ModelState.Remove(nameof(obj.ShortBioRu));

            if (ModelState.IsValid)
            {
                _db.Persons.Add(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0){
                return NotFound();
            }

            var PersonFromDb = _db.Persons.Find(id);

            if (PersonFromDb == null){
                return NotFound();
            }

            return View(PersonFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPOST(Person obj)
        {   
            ModelState.Remove(nameof(obj.ShortBioEn));
            ModelState.Remove(nameof(obj.ShortBioRu));

            if (ModelState.IsValid){
                _db.Persons.Update(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0){
                return NotFound();
            }

            var PersonFromDb = _db.Persons.Find(id);

            if (PersonFromDb == null){
                return NotFound();
            }

            return View(PersonFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int id)
        {   
            var obj = _db.Persons.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Persons.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}