using DomainModels;
using InfraStructure.Implementation;
using InfraStructure.Interfaces;
using InfraStructure.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace QuizApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        //Action for Categories list
        public async Task<IActionResult> Categories(int pg=1)
        {
            return View(await _categoryRepository.CategoriesList(pg));
        }


        //Action for category Create
        public async Task<IActionResult> CategoryCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CategoryCreate(CategoryCreateVm model)
        {
            if (ModelState.IsValid)
            {
                if (await _categoryRepository.CategoryCreate(model))
                {
                    ModelState.AddModelError("", "Category create succesfully");
                }
                else
                {
                    ModelState.AddModelError("", "something wents wrong");
                }
                //return View();
                return RedirectToAction("Categories", new { Controller = "Category" });

            }
            return View();
        }

        //Action for Category Delete
        public async Task<IActionResult> CategoryDelete(int id)
        {
            if (await _categoryRepository.CategoryDelete(id))
            {
                ModelState.AddModelError("", "Category Delete succesfully");
            }
            else
            {
                ModelState.AddModelError("", "Something wents wrong");
            }
            return RedirectToAction("Categories", new { Controller = "Category" });
        }

        //Action for Category get by id
        public async Task<IActionResult> CategoryUpdate(int id)
        {
            return View(await _categoryRepository.CategoryGetById(id));
        }


        //Action for Category update
        [HttpPost]
        public async Task<IActionResult> CategoryUpdate(CategoryUpdateVm model)
        {
            await _categoryRepository.CategoryUpdate(model);
            return RedirectToAction("Categories", new { Controller = "Category" });
        }

        //Action for category questions
        public async Task<IActionResult> CategoryQuestions(int id)
        {
            TempData["categoryId"] = id;
            TempData.Keep("categoryId");
            return View(await _categoryRepository.CategoryQuestions(id));
        }

        //Action for category Questions Delete
        public async Task<IActionResult> CategoryQuestionDelete(int id)
        {
            if (await _categoryRepository.CategoryQuestionDelete(id))
            {
                ModelState.AddModelError("", "Question Delete successfully");
            }
            else
            {
                ModelState.AddModelError("", "Something wents wrong");
            }
            // return RedirectToAction("CategoryQuestions",TempData["categoryId"]);
            return View(await _categoryRepository.CategoryQuestions((int)TempData["categoryId"]));
        }

    }
}
