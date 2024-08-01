using Microsoft.AspNetCore.Mvc;
using MoeKinoWebApp.Data;
using MoeKinoWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace MvcApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
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