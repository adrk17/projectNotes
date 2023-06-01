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

        public IActionResult Privacy()
        {
           
            var usr = _dbContext.Users.Find(1);
            Console.WriteLine(usr.ID);
            Console.WriteLine(usr.Username);
            Console.WriteLine(usr.Password);
             
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}