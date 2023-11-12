using DomainModels;
using InfraStructure.Implementation;
using InfraStructure.Interfaces;
using InfraStructure.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace QuizApp.Controllers
{
    public class ExamController : Controller
    {
        private readonly IQuizRepository _quizRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ExamController(IQuizRepository quizRepository, ICategoryRepository categoryRepository)
        {
            _quizRepository = quizRepository;
            _categoryRepository = categoryRepository;
        }

        //Action for Quiz list
        public async Task<IActionResult> SetExamList(int pg=1)
        {
            return View(await _quizRepository.Exams(pg));
        }

        //Action for Quiz Create
        public async Task<IActionResult> SetExam()
        {
            return View(new SetExamCreateVm()
            {
                Categories = await _categoryRepository.Categories()
            }); 
        }

        //Action for Quiz Create
        [HttpPost]
        public async Task<IActionResult> SetExam(SetExamCreateVm model)
        {
             var (success, questionsCount) =await _quizRepository.SetExam(model);
                if (success)
                {
                    ModelState.AddModelError("", "Exam create successfully");
                }
                else
                {
                
                string massage = "Total questions are: " + questionsCount;
                ViewBag.errorMessage1=massage;
                ViewBag.errorMessage = "Error: Number of questions exceeds the available questions.";
                return View(new SetExamCreateVm()
                {
                    Categories = await _categoryRepository.Categories()
                });
            }
                //return View();
                return RedirectToAction("SetExamList", new { Controller = "Exam" });

        }

        //Action for Quiz delete
        public async Task<IActionResult> ExamDelete(int id)
        {
            if (await _quizRepository.ExamDelete(id))
            {
                ModelState.AddModelError("", "Question Delete successfully");
            }
            else
            {
                ModelState.AddModelError("", "Something wents wrong");
            }
            return RedirectToAction("SetExamList", new { Controller = "Exam" });
        }

        //Action for Quiz Results -----admin panel
        public async Task<IActionResult> Results()
        {
            return View(await _quizRepository.Examslist());

        }

        [HttpPost]
        public async Task<IActionResult> Results(SetExamVm model)
        {
            string quizName = Request.Form["resultList"].ToString();

            if (quizName != null)
            {
                HttpContext.Session.SetString("quizName",quizName);
               // TempData["quizName"] = quizName;
                return RedirectToAction("ShowResults");
            }
            else
            {
                return View();
            }
        }

        //Action for Quiz Results-----teacher panel
        public async Task<IActionResult> Results2()
        {
            return View(await _quizRepository.Examslist());
        }

        [HttpPost]
        public async Task<IActionResult> Results2(SetExamVm model)
        {
            string quizName = Request.Form["resultList"].ToString();

            if (quizName != null)
            {
                HttpContext.Session.SetString("quizName", quizName);
                //TempData["quizName"] = quizName;

                return RedirectToAction("ShowResults2");
            }
            else
            {
                return View();
            }
        }


        //Action for show Results  ------for admin panel
        public async Task<IActionResult> ShowResults(int pg=1)
        {
            return View(await _quizRepository.ExamGetByName((string)HttpContext.Session.GetString("quizName"),pg));

        }

        //Action for show Results ------for teacher panel
        public async Task<IActionResult> ShowResults2(int pg=1)
        {
            return View(await _quizRepository.ExamGetByName((string)HttpContext.Session.GetString("quizName"),pg));

        }


    }
}
