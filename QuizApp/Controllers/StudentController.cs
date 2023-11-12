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
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly QuizAppContext db;

        public StudentController(IStudentRepository studentRepository, IWebHostEnvironment hostEnvironment, QuizAppContext quizAppContext)
        {
            _studentRepository = studentRepository;
            _hostEnvironment = hostEnvironment;
            db = quizAppContext;
        }

        //student dashboard
        [Route("Student Dashboard")]
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("StudentLogin", "Login");
            }
            var username = HttpContext.Session.GetString("Username");
            var student = db.Students.Where(p => p.Username == username).FirstOrDefault();
            var StudentModel = new StudentVm()
            {
                Id = student.Id,
                Name = student.Name,
                Username = student.Username,
                Password = student.Password,
                Image = student.Image,
            };
           // TempData["model"] = StudentModel;
            HttpContext.Session.SetObject("StudentData", StudentModel);


            return View();
        }

        //Action for students list ------For admin panel
        [Route("Students")]
        public async Task<IActionResult> Students(int pg=1)
        {
            return View(await _studentRepository.StudentsList(pg));
        }

        //Action for student Create
       
        public async Task<IActionResult> StudentCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StudentCreate(StudentCreateVm model)
        {
            if (!(await _studentRepository.IsImageFile(model.ImageFile)))
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

            if (await _studentRepository.StudentCreate(model))
            {
                ModelState.AddModelError("", "Student create succesfully");
            }
            else
            {
                ModelState.AddModelError("", "something wents wrong");
            }
          
            return RedirectToAction("Students", new { Controller = "Student" });
        }

        //Action for student Delete
        public async Task<IActionResult> StudentDelete(int id)
        {
            if (await _studentRepository.StudentDelete(id))
            {
                ModelState.AddModelError("", "Student Delete succesfully");
            }
            else
            {
                ModelState.AddModelError("", "Something wents wrong");
            }
            return RedirectToAction("Students", new { Controller = "Student" });
        }

        //Action for student get by id ----  Admin pannel
        
        public async Task<IActionResult> StudentUpdate(int id)
        {
            return View(await _studentRepository.StudentGetById(id));
        }


        //Action for student update ------- Admin pannel
        [HttpPost]
        public async Task<IActionResult> StudentUpdate(StudentUpdateVm model)
        {
            await _studentRepository.StudentUpdate(model);
            return RedirectToAction("Students", new { Controller = "Student" });
        }

        //Action for student get by id   ------- Student pannel
        
        public async Task<IActionResult> StudentUpdateForstudent(int id)
        {
            return View(await _studentRepository.StudentGetById(id));
        }


        //Action for student update   ------- Student pannel
        [HttpPost]
        public async Task<IActionResult> StudentUpdateForstudent(StudentUpdateVm model)
        {
            await _studentRepository.StudentUpdate(model);
            return RedirectToAction("Dashboard", new { Controller = "Student" });
        }

        //Action for student details for admin
        
        public async Task<IActionResult> StudentDetails(int id)
        {
            return View(await _studentRepository.StudentDetails(id));
        }

        //Action for student details for student
       
        public async Task<IActionResult> StudentDetailsForStudent(int id)
        {
            return View(await _studentRepository.StudentDetails(id));
        }

        //Action for Start exam
       
        public async Task<IActionResult> StartExam()
        {
             return View();
        }


        //Action for Start exam
        [HttpPost]
        public async Task<IActionResult> StartExam(string roomCode)
        {
            // student model from dashboard
            var student = HttpContext.Session.GetObject<StudentVm>("StudentData");

            //retrive mark object from db to check that quiz is already attemt or not
            var markObject = await db.Marks.FirstOrDefaultAsync(p => p.StudentId == student.Id && p.ExamCode==roomCode);
           
            if(markObject!=null)
            {
               // var roomcode = markObject.ExamCode;
                var marks = markObject.Marks;

                ViewData["errorMsg"] = "You have already attempted the quiz.";
                ViewData["marks"] = marks;
                return View();
            }
            else
            {
                //list of questions to store quiz qs
                var examQuestions = new List<QuestionVm>();

                //retrive quiz from db for no. of questions and quizId
                var quiz = await db.SetExams.Where(p => p.RoomCode == roomCode).FirstOrDefaultAsync();  //retrive quiz from room code 

                if (quiz != null)
                {
                    //quizId need for submitquiz action
                    TempData["quizId"] = quiz.Examid;

                    //no of questions need for submitquiz view
                    TempData["quiz"] = quiz.NoOfQuestions;

                    //roomcode need for submitquiz action
                    TempData["roomCode"] = roomCode;

                    examQuestions = (List<QuestionVm>)await _studentRepository.StartExam(roomCode);
                    if (examQuestions != null)
                    {

                        //send questions of quiz to view
                        HttpContext.Session.SetObject("Questions", examQuestions);
                        return RedirectToAction("AttemptQuiz", new { Controller = "Student" });
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    ViewData["errorMsg2"] = "Room code is invalid.";
                    return View();
                }

               
            }
           
        }

        //Action for AttemptQuiz
        
        public async Task<IActionResult> AttemptQuiz()
        {
            var retrievedQuestions = HttpContext.Session.GetObject<List<QuestionVm>>("Questions");
            if (retrievedQuestions!=null)
            {
                return View();
            }
            else
            {
                TempData["errorMsg"] = "No questions found";
                return View();
            }
        }

        //Action for SubmitQuiz
        
        [HttpPost]
        public async Task<IActionResult> SubmitQuiz(List<string> answers)
        {
            var correctAnswers = HttpContext.Session.GetObject<List<QuestionVm>>("Questions");

            // student model from dashboard
            var student = HttpContext.Session.GetObject<StudentVm>("StudentData");

            //quizId from setExam action
            var quizId = TempData["quizId"];

            //roomCode from setExam action
            var roomCode = TempData["roomCode"];


            if (correctAnswers == null || answers == null || correctAnswers.Count != answers.Count)
            {
                ViewData["errorMsg"] = "Invalid quiz submission.";
                return View("Error"); // Handle the error scenario appropriately in your view.
            }

            int marks = 0;
            for (int i = 0; i < correctAnswers.Count; i++)
            {
                if (answers[i] == correctAnswers[i].CorrectAns)
                {
                    marks++;
                }
            }
            ViewData["Marks"] = marks; // Pass the marks to the view if you want to display it.

            //now save marks in db
            var mark = new Mark()
            {
                StudentId=student.Id,
                ExamId= (int?)quizId,
                ExamCode= (string)roomCode,
                Marks=marks,
            };

            await db.Marks.AddAsync(mark);
            db.SaveChanges();

            return View(); // Display the result view where you can show the marks.
        }







    }
}
