using InfraStructure.Implementation;
using InfraStructure.Interfaces;
using InfraStructure.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace QuizApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        //Action for admins Dashboard
        [Route("Dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("AdminLogin", "Login", new { area = "" });
            }
            return View();
        }

        //Action for admins list
        
        public async Task<IActionResult> Admins(int pg = 1)
        {
            return View(await _adminRepository.AdminsList(pg));
        }

        //Action for admin Create

       
        public async Task<IActionResult> AdminCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AdminCreate(AdminCreateVm model)
        {
            if (ModelState.IsValid)
            {
                if (await _adminRepository.AdminCreate(model))
                {
                    ModelState.AddModelError("", "Admin create succesfully");
                }
                else
                {
                    ModelState.AddModelError("", "something wents wrong");
                }
                //return View();
                return RedirectToAction("Admins", new { Controller = "Admin" });

            }
            return View();
        }

        //Action for admin name duplication checking
        public async Task<IActionResult> IsNameExist(string name)
        {
            return Json(!await _adminRepository.IsNameExist(name));
        }


        //Action for admin Delete
        
        public async Task<IActionResult> AdminDelete(int id)
        {
            if (await _adminRepository.AdminDelete(id))
            {
                ModelState.AddModelError("", "Admin Delete succesfully");
            }
            else
            {
                ModelState.AddModelError("", "You can't delete last admin");
            }
            return RedirectToAction("Admins", new { Controller = "Admin" });
        }





    }
}
