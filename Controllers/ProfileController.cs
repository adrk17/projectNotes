using Microsoft.AspNetCore.Mvc;
using projectNotes.Database;
using projectNotes.Models;
using projectNotes.Utils.Encryption;

namespace projectNotes.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDatabaseContext _dbContext;
        private readonly PasswordHash _passwordHash = new PasswordHash("sha256");

        public ProfileController(ApplicationDatabaseContext dbcontext)
        {
            _dbContext = dbcontext;
        }
        
        public IActionResult Index()
        {
            return View("Index", new ProfileModel() {Username = HttpContext.Session.GetString("username")});
        }

        public IActionResult ChangePassword(ProfileModel model)
        {
            model.Username = HttpContext.Session.GetString("username");
            if (ModelState.IsValid)
            {
                // did we find the user in the database?
                var userInDB = _dbContext.Users.Where(u => u.Username == model.Username).FirstOrDefault();

                bool passwordMatch = _passwordHash.ComparePasswords(model.Password, userInDB.Password);
                Console.WriteLine(passwordMatch);
                if (passwordMatch)
                {
                    if(model.NewPassword == model.NewPasswordRe)
                    {
                        
                        userInDB.Password = _passwordHash.HashPassword(model.NewPassword);
                        if(!_passwordHash.ComparePasswords(model.Password, userInDB.Password))
                        {
                            _dbContext.SaveChanges();
                            return View("Index", new ProfileModel() { Username = HttpContext.Session.GetString("username"), SuccessMsg = "Password changed successfully!" });
                        }
                        else
                        {
                            
                            ModelState.AddModelError("Password", "Old and new passwords are the same!");
                            return View("Index", model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("NewPassword", "New passwords do not match");
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "Invalid password");
                }
                return View("Index", model);
            }
            Console.WriteLine("not valid");
            return View("Index", model);
        }
    }
}
