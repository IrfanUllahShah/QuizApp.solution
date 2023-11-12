using InfraStructure.Implementation;
using InfraStructure.Interfaces;
using InfraStructure.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace QuizApp.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ICategoryRepository _categoryRepository;


        public QuestionController(IQuestionRepository questionRepository,ICategoryRepository categoryRepository)
        {
            _questionRepository = questionRepository;
            _categoryRepository = categoryRepository;
        }

        //Action for Questions list
        public async Task<IActionResult> Questions(int pg=1)
        {
            return View(await _questionRepository.Questions(pg));
        }

        //Action for Questions Create

        public async Task<IActionResult> QuestionCreate()
        {
            return View(new QuestionCreateVm()
            {
                Categories =await _categoryRepository.Categories(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> QuestionCreate(QuestionCreateVm model)
        {
            if (ModelState.IsValid)
            {
                if (await _questionRepository.QuestionCreate(model))
                {
                    ModelState.AddModelError("", "Question create successfully");
                }
                else
                {
                    ModelState.AddModelError("", "something wents wrong");
                }
                //return View();
                return RedirectToAction("Questions", new { Controller = "Question" });

            }
            return View(new QuestionCreateVm()
            {
                Categories = await _categoryRepository.Categories(),
            });
        }

        //Action for Questions Delete
        public async Task<IActionResult> QuestionDelete(int id)
        {
            if (await _questionRepository.QuestionDelete(id))
            {
                ModelState.AddModelError("", "Question Delete successfully");
            }
            else
            {
                ModelState.AddModelError("", "Something wents wrong");
            }
            return RedirectToAction("Questions", new { Controller = "Question" });
        }

        //Action for Questions get by id
        public async Task<IActionResult> QuestionUpdate(int id)
        {
            return View(await _questionRepository.QuestionGetById(id));
        }


        //Action for Questions update
        [HttpPost]
        public async Task<IActionResult> QuestionUpdate(QuestionUpdateVm model)
        {
            await _questionRepository.QuestionUpdate(model);
            return RedirectToAction("Questions", new { Controller = "Question" });
        }

    }
}
