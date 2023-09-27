using DomainModels;
using InfraStructure.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace QuizApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly QuizAppContext db;

        public StudentController(QuizAppContext quizAppContext)
        {
            db = quizAppContext;
        }
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("StudentLogin", "Login");
            }
            return View();
        }
       
    }
}
