using Microsoft.AspNetCore.Mvc;
using projectNotes.Database;
using projectNotes.Models;
using projectNotes.Utils.Encryption;

namespace projectNotes.Controllers
{
    public class LoginRegisterController : Controller
    {
        private readonly ApplicationDatabaseContext _dbContext;
        private readonly PasswordHash _passwordHash = new PasswordHash("sha256");
        public LoginRegisterController(ApplicationDatabaseContext dbcontext)
        {
            _dbContext = dbcontext;
        }
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// GET action for the Login view
        /// </summary>
        /// <param name="model"> User model sent from the View</param>
        /// <param name="action"> Name of the action possible values - Login, Register</param>
        /// <returns>Depending on the validation, it can either be Index(LoginRegister) View with validation errors or a LoginSuccess View </returns>
        [HttpPost]
        [ActionName("Login")]
        public IActionResult Login(User model, string action)
        {
            Console.WriteLine(action+" action");
            if (ModelState.IsValid)
            {
                var userInDB = _dbContext.Users.Where(u => u.Username == model.Username).FirstOrDefault();

                if(action == "Login")
                {
                    return LoginLogic(model, userInDB);
                }
                
                if(action == "Register")
                {
                    return RegisterLogic(model, userInDB);
                }

                Console.WriteLine("Invalid action");
                ModelState.AddModelError("ID", "Invalid action");
                return View("Index", model);
            }
            // If the ModelState is not valid, return the Login view with the validation errors
            return View("Index", model);
        }

        private IActionResult LoginLogic(User model, User? userInDB)
        {
            // did we find the user in the database?
            if(userInDB == null) {
                Console.WriteLine("User not found");
                ModelState.AddModelError("ID", "Invalid login or password");
                return View("Index", model); 
            }

            bool passwordMatch = _passwordHash.ComparePasswords(model.Password, userInDB.Password);

            if (passwordMatch)
            {
                Console.WriteLine("Login successfull");
                HttpContext.Session.SetString("username", model.Username);
                HttpContext.Session.SetInt32("userID", userInDB.ID);

                return View("LoginSuccess", new LoginSuccessViewModel() { Username = model.Username, Login = true });
            }

            Console.WriteLine("Hashes incompatible!");
            ModelState.AddModelError("ID", "Invalid login or password");
            return View("Index", model);

        }
        private IActionResult RegisterLogic(User model, User? userInDB)
        {
            // did we find the user in the database?
            if(userInDB != null)
            {
                Console.WriteLine("User already in db");
                ModelState.AddModelError("ID", "User already exists!");
                return View("Index", model);
            }
          
            model.Password = _passwordHash.HashPassword(model.Password);
            
            _dbContext.Users.Add(model);
            _dbContext.SaveChanges();

            Console.WriteLine("Registration successfull");

            // Getting ID that the database has henerated in order to log in the new user
            userInDB = _dbContext.Users.Where(u => u.Username == model.Username).FirstOrDefault();
            // Error handling in case of database malfunction
            if(userInDB == null) {
                Console.WriteLine("Database error, could not find the new user in the db");
                ModelState.AddModelError("ID", "Database error, could not find the new user in the db");
                return View("Index", model);
            }
            HttpContext.Session.SetInt32("userID", userInDB.ID);
            HttpContext.Session.SetString("username", userInDB.Username);

            return View("LoginSuccess", new LoginSuccessViewModel() { Username = model.Username, Login = false });
        }


        public IActionResult LoginSuccess(LoginSuccessViewModel vm)
        {
            return View(vm);
        }

        public IActionResult Logout()
        {
            HttpContext?.Session.Clear();
            return View("Index");
        }
    }
}
