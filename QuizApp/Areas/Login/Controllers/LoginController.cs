using DomainModels;
using InfraStructure.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace QuizApp.Areas.Login.Controllers
{
    [Area("Login")]
    public class LoginController : Controller
    {
        private readonly QuizAppContext db;

        public LoginController(QuizAppContext quizAppContext)
        {
            db = quizAppContext;
        }

        //----------------------Admin login system----------------------------
        
        public IActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AdminLogin(LoginVm model)
        {
            if (ModelState.IsValid)
            {
                var result = db.Admins.Where(p => p.Username == model.username && p.Password == model.password).FirstOrDefault();
                if (result == null)
                {
                    ViewBag.errorMessage = "Invalid username or password";
                    return View();
                }
                else
                {

                    HttpContext.Session.SetString("Username", model.username);
                    TempData["Username"] = HttpContext.Session.GetString("Username");
                    return RedirectToAction("Dashboard", "Admin", new {area=""});
                }

            }
            return View();
        }


        //----------------------Teacher login system----------------------------
       
        public IActionResult TeacherLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult TeacherLogin(LoginVm model)
        {
            if (ModelState.IsValid)
            {
               // var result = db.Teachers.FirstOrDefault(p => p.Username.Equals(model.username, StringComparison.OrdinalIgnoreCase) && p.Password == model.password);
                var result = db.Teachers.Where(p => p.Username == model.username && p.Password == model.password).FirstOrDefault();
                if (result == null)
                {
                    ViewBag.errorMessage = "Invalid username or password";
                    return View();
                }
                else
                {
                    HttpContext.Session.SetString("Username", model.username);
                    TempData["Username"] = HttpContext.Session.GetString("Username");
                    return RedirectToAction("Dashboard", "Teacher" , new { area = "" });
                }

            }
            return View();
        }


        //----------------------Student login system----------------------------
        
        public IActionResult StudentLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult StudentLogin(LoginVm model)
        {
            if (ModelState.IsValid)
            {
                var result = db.Students.Where(p => p.Username == model.username && p.Password == model.password).FirstOrDefault();
                if (result == null)
                {
                    ViewBag.errorMessage = "Invalid username or password";
                    return View();
                }
                else
                {
                    HttpContext.Session.SetString("Username", model.username);
                    TempData["Username"] = HttpContext.Session.GetString("Username");
                    return RedirectToAction("Dashboard", "Student", new { area = "" });
                }

            }
            return View();
        }


        //----------------------Admin logout system----------------------------
        [Route("AdminLogout")]
        public IActionResult AdminLogout()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Index", "Home", new {area =""});
        }

        //----------------------Teacher logout system----------------------------
        [Route("TeacherLogout")]
        public IActionResult TeacherLogout()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Index", "Home", new {area = ""});
        }

        //----------------------Student logout system----------------------------
        [Route("StudentLogout")]
        public IActionResult StudentLogout()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Index", "Home", new {area = ""});
        }
    }
}
