using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MoeKinoWebApp.Data;
using MoeKinoWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace MvcApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class MovieImageController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MovieImageController(ApplicationDbContext db){
            _db = db;
        } 

        public IActionResult Index()
        {
            IEnumerable<MovieImage> objMovieImageList = _db.MovieImages;
            return View(objMovieImageList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST(MovieImage obj)
        {   
            var files = HttpContext.Request.Form.Files;

            if (files.Count > 0)
            {
                var file = files[0];
                if (file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        obj.Image = memoryStream.ToArray();
                    }
                }
            }
            _db.MovieImages.Add(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0){
                return NotFound();
            }

            var MovieImageFromDb = _db.MovieImages.Find(id);

            if (MovieImageFromDb == null){
                return NotFound();
            }

            var base64String = Convert.ToBase64String(MovieImageFromDb.Image);
            var imageData = $"data:image/jpeg;base64,{base64String}";

            ViewData["ImageBase64"] = imageData;

            return View(MovieImageFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int id)
        {   
            var obj = _db.MovieImages.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.MovieImages.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}