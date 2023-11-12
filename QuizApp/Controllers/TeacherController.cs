using DomainModels;
using InfraStructure.Implementation;
using InfraStructure.Interfaces;
using InfraStructure.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace QuizApp.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly QuizAppContext db;

        public TeacherController(ITeacherRepository teacherRepository, IWebHostEnvironment hostEnvironment, QuizAppContext quizAppContext)
        {
            _teacherRepository = teacherRepository;
            _hostEnvironment = hostEnvironment;
            db = quizAppContext;
        }

        //teacher dashboard
        [Route("Teacher Dashboard")]
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("TeacherLogin", "Login");
            }
            var username = HttpContext.Session.GetString("Username");
            var teacher = db.Teachers.Where(p => p.Username == username).FirstOrDefault();
            var TeacherModel = new TeacherVm()
            {
                Id = teacher.Id,
                Name = teacher.Name,
                Username = teacher.Username,
                Password = teacher.Password,
                Image = teacher.Image,
            };
            // TempData["model"] = StudentModel;
            HttpContext.Session.SetObject("TeacherData", TeacherModel);


            return View();
        }

        //Action for teachers list
        
        public async Task<IActionResult> Teachers(int pg = 1)
        {
            return View(await _teacherRepository.TeachersList(pg));
        }

        //Action for teacher Create
       
        public async Task<IActionResult> TeacherCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TeacherCreate(TeacherCreateVm model)
        {
            if (!(await _teacherRepository.IsImageFile(model.ImageFile)))
            {
                //ModelState.AddModelError("", "Only image files are allowed.");
                ViewBag.ErrorMessage = "Only image files are allowed.";
                return View(model); // Return to the view with the validation error
            }

            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
            string extension = Path.GetExtension(model.ImageFile.FileName);
            model.Image = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/Images/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(fileStream);
            }

            if (await _teacherRepository.TeacherCreate(model))
            {
                ModelState.AddModelError("", "Teacher create succesfully");
            }
            else
            {
                ModelState.AddModelError("", "something wents wrong");
            }
            
            return RedirectToAction("Teachers", new { Controller = "Teacher" });
        }

        //Action for teacher Delete
        public async Task<IActionResult> TeacherDelete(int id)
        {
            if (await _teacherRepository.TeacherDelete(id))
            {
                ModelState.AddModelError("", "teacher Delete successfully");
            }
            else
            {
                ModelState.AddModelError("", "Something wents wrong");
            }
            return RedirectToAction("Teachers", new { Controller = "Teacher" });
        }

        //Action for teacher get by id ----  Admin pannel
        
        public async Task<IActionResult> TeacherUpdate(int id)
        {
            return View(await _teacherRepository.TeacherGetById(id));
        }


        //Action for teacher update ------- Admin pannel
        [HttpPost]
        public async Task<IActionResult> TeacherUpdate(TeacherUpdateVm model)
        {
            await _teacherRepository.TeacherUpdate(model);
            return RedirectToAction("Teachers", new { Controller = "Teacher" });
        }

        //Action for student get by id   ------- Teacher pannel
        public async Task<IActionResult> TeacherUpdateForTeacher(int id)
        {
            return View(await _teacherRepository.TeacherGetById(id));
        }


        //Action for student update   ------- Teacher pannel
        [HttpPost]
        public async Task<IActionResult> TeacherUpdateForTeacher(TeacherUpdateVm model)
        {
            await _teacherRepository.TeacherUpdate(model);
            return RedirectToAction("Dashboard", new { Controller = "Teacher" });
        }

        //Action for teacher details for admin pannel
        public async Task<IActionResult> TeacherDetails(int id)
        {
            return View(await _teacherRepository.TeacherDetails(id));
        }

        //Action for Teacher details for teacher panel
        public async Task<IActionResult> TeacherDetailsForTeacher(int id)
        {
            return View(await _teacherRepository.TeacherDetails(id));
        }

    }
}
