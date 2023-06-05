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
                else if(action == "Register")
                {
                    return RegisterLogic(model, userInDB);
                }
                else
                {
                    Console.WriteLine("Invalid action");
                    ModelState.AddModelError("ID", "Invalid action");
                    return View("Index", model);
                }
                


            }
            // If the ModelState is not valid, return the Login view with the validation errors
            return View("Index", model);
        }

        private IActionResult LoginLogic(User model, User? userInDB)
        {
            if(userInDB == null) {
                Console.WriteLine("User not found");
                ModelState.AddModelError("ID", "Invalid login or password");
                return View("Index", model); 
            }
            var modelPasswordHash = _passwordHash.Hash(model.Password);
            model.Password = System.Text.Encoding.UTF8.GetString(modelPasswordHash);
            var userInDBPasswordHash = System.Text.Encoding.UTF8.GetBytes(userInDB.Password);
            bool passwordMatch = _passwordHash.CompareHashes(System.Text.Encoding.UTF8.GetBytes(model.Password), userInDBPasswordHash);
            if (passwordMatch)
            {
                Console.WriteLine("Login successfull");
                HttpContext.Session.SetString("username", model.Username);


                return View("LoginSuccess", new LoginSuccessViewModel() { Username = model.Username, Login = true });
            }
            else
            {
                Console.WriteLine("Hashes incompatible!");
                ModelState.AddModelError("ID", "Invalid login or password");
                return View("Index", model);
            }
        }
        private IActionResult RegisterLogic(User model, User? userInDB)
        {
            if(userInDB != null)
            {
                Console.WriteLine("User already in db");
                ModelState.AddModelError("ID", "User already exists!");
                return View("Index", model);
            }
            byte[] modelPasswordHash = _passwordHash.Hash(model.Password);
            model.Password = System.Text.Encoding.UTF8.GetString(modelPasswordHash);
            _dbContext.Users.Add(model);
            _dbContext.SaveChanges();

            Console.WriteLine("Registration successfull");
            HttpContext.Session.SetString("username", model.Username);

            return View("LoginSuccess", new LoginSuccessViewModel() { Username = model.Username, Login = false });

        }


        public IActionResult LoginSuccess(LoginSuccessViewModel vm)
        {
            return View(vm);
        }
    }
}
