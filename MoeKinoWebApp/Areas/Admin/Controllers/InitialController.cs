using Microsoft.AspNetCore.Mvc;
using MoeKinoWebApp.Data;
using MoeKinoWebApp.Models;

namespace MvcApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InitialController : Controller
    {
        private readonly ApplicationDbContext _db;

        public InitialController(ApplicationDbContext db){
            _db = db;
        } 

        public IActionResult Index()
        {
            return View();
        }
    }
}