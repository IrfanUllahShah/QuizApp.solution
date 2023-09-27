using DomainModels;
using InfraStructure.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace QuizApp.Controllers
{
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
                    return RedirectToAction("Dashboard", "Admin");
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
                    return RedirectToAction("Dashboard", "Student");
                }

            }
            return View();
        }


        //----------------------Admin logout system----------------------------
        public IActionResult AdminLogout()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Index", "Home");
        }

        //----------------------Student logout system----------------------------
        public IActionResult StudentLogout()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Index", "Home");
        }
    }
}
