using Microsoft.AspNetCore.Mvc;
using projectNotes.Database;
using projectNotes.Models;
using projectNotes.Utils.Encryption;

namespace projectNotes.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDatabaseContext _dbContext;

        public ProfileController(ApplicationDatabaseContext dbcontext)
        {
            _dbContext = dbcontext;
        }
        
        public IActionResult Index()
        {
            return View("Index", new ProfileModel() {Username = HttpContext.Session.GetString("username")});
        }
    }
}
