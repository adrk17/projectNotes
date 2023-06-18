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
            var vm = new IndexViewModel();
            vm.CompleteNotes = GetNotes();
            foreach(var cn in vm.CompleteNotes)
            {
                Console.WriteLine(cn.ToString());
            }
            return View(vm);
        }

        public IActionResult AddNote(IndexViewModel model)
        {
            model.NewNote.Tags = new();
            model.NewNote.Categories = new();
            Console.WriteLine(model.NewNote.ToString());
            if (ModelState.IsValid)
            {
                Console.WriteLine("Model is valid");
                var userId = HttpContext.Session.GetInt32("userID");
                if (userId != null)
                {
                    Console.WriteLine("User ID: " + userId + " is adding a note");
                    model.NewNote.Note.UserID = (int)userId;
                    model.NewNote.Note.Created_at = DateTime.Now;
                    model.NewNote.Note.Updated_at = DateTime.Now;
                    _dbContext.Notes.Add(model.NewNote.Note);
                    _dbContext.SaveChanges();

                    model.CompleteNotes = GetNotes();
                    return View("Index", model);
                }
            }
            Console.WriteLine("Model not valid");
            var indexViewModel = new IndexViewModel();
            return View("Index", indexViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private List<NoteComplete> GetNotes()
        {
            var userID = HttpContext.Session.GetInt32("userID");
            var notes = _dbContext.Notes.Where(n => n.UserID == userID).ToList();
            var noteCompletes = new List<NoteComplete>();
            foreach (var note in notes)
            {
                var noteComplete = new NoteComplete();
                noteComplete.Note = note;
                var tagQuerry = from t in _dbContext.Tags
                                join nt in _dbContext.NoteTags on t.ID equals nt.TagID
                                where nt.NoteID == note.ID
                                select t;
                noteComplete.Tags = tagQuerry.ToList();
                
                var categoryQuerry = from c in _dbContext.Categories
                                     join nc in _dbContext.NoteCategories on c.ID equals nc.CategoryID
                                     where nc.NoteID == note.ID
                                     select c;
                noteComplete.Categories = categoryQuerry.ToList();
                noteCompletes.Add(noteComplete);
            }

            return noteCompletes;
        }
    }
}