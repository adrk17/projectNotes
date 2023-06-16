using Microsoft.AspNetCore.Mvc;
using projectNotes.Database;
using projectNotes.Models;
using System.Diagnostics;

namespace projectNotes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDatabaseContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDatabaseContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext; // This is the database context that we create
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult AddNote(Note model)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("Model is valid");
                var userId = HttpContext.Session.GetInt32("userID");
                if (userId != null)
                {
                    Console.WriteLine("User ID: " + userId + " is adding a note");
                    model.UserID = (int)userId;
                    model.Created_at = DateTime.Now;
                    model.Updated_at = DateTime.Now;
                    _dbContext.Notes.Add(model);
                    _dbContext.SaveChanges();
                    return View("Index");
                }
            }

            return View("Index", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}