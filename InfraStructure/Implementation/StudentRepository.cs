using DomainModels;
using InfraStructure.Interfaces;
using InfraStructure.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using InfraStructure.Pagination;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace InfraStructure.Implementation
{
    public class StudentRepository : IStudentRepository
    {
        private readonly QuizAppContext db;
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;

        public StudentRepository(QuizAppContext quizAppContext,IGenericRepository genericRepository,IMapper mapper)
        {
            db = quizAppContext;
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        //Students list
        public async Task<ListPagerVm<StudentVm>> StudentsList(int currentPage)
        {
            var listPager = new ListPagerVm<StudentVm>();  //make object of viewModel

            // Apply pagination on records
            var allItems = await _genericRepository.GetAll<Student>();
            const int pageSize = 3;
            if (currentPage < 1)
            {
                currentPage = 1;
            }
            int recordsCount = allItems.Count;

            // object of Pager class
            var pager = new Pager(recordsCount, currentPage, pageSize);

            //pager for sending to view
            listPager.pager = pager;

            //skip records
            int recordsSkip = (currentPage - 1) * pageSize;

            //select questions for view from all records
            var result = new List<StudentVm>();
            var students = allItems.Skip(recordsSkip).Take(pager.PageSize).ToList();
            if (students != null && students.Count > 0)
            {
                foreach (var item in students)
                {
                    result.Add(new StudentVm()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Username = item.Username,
                        Password = item.Password,
                        Image = item.Image,
                        //Image=item.Image,
                    });
                }
                listPager.list = result;
                return listPager;
            }
            return listPager;
        }

        //method for checking image file
        public async Task<bool> IsImageFile(IFormFile file)
        {
            if (file == null)
            {
                return false;
            }

            // Check if the file has a valid image extension
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".ico", ".jfif", ".webp" };
            string extension = Path.GetExtension(file.FileName).ToLower();

            return allowedExtensions.Contains(extension);
        }

        //Create Student
        public async Task<bool> StudentCreate(StudentCreateVm model)
        {
            var student = new Student()
            {
                Name = model.Name,
                Password = model.Password,
                Username = model.Username,
                Image = model.Image,
            };
          //  Student student = _mapper.Map<Student>(model);
            await _genericRepository.Create<Student>(student);
            return true;

        }

        //Delete Student
        public async Task<bool> StudentDelete(int id)
        {
            var student = await db.Students.Where(p => p.Id == id).FirstOrDefaultAsync();
            _genericRepository.Delete<Student>(student);
            return true;
        }

        //Get by id Student
        public async Task<StudentUpdateVm> StudentGetById(int id)
        {
            var student = await db.Students.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (student != null)
            {
                return new StudentUpdateVm()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Password = student.Password,
                    Username = student.Username,
                    Image = student.Image,
                };
            }
            else
            {
                return null;
            }
        }

        //update Student
        public async Task<bool> StudentUpdate(StudentUpdateVm model)
        {
            var student = await db.Students.Where(p => p.Id == model.Id).FirstOrDefaultAsync();
            if (student != null)
            {
                student.Id = model.Id;
                student.Name = model.Name;
                student.Password = model.Password;
                student.Username = model.Username;
                student.Image = model.Image;

            }
            _genericRepository.Update<Student>(student);
            return true;
        }

        //details ofStudent
        public async Task<StudentVm> StudentDetails(int id)
        {
            var student = await db.Students.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (student != null)
            {
                return new StudentVm()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Password = student.Password,
                    Username = student.Username,
                    Image = student.Image,
                };
            }
            else
            {
                return null;
            }
        }

        // start exam
        public async Task<IEnumerable<QuestionVm>> StartExam(string roomCode)
        {
            var quiz = await db.SetExams.Where(p => p.RoomCode == roomCode).FirstOrDefaultAsync();
            var questions = await db.Questions.Include(p => p.Category).Where(p => p.CategoryId == quiz.CategoryId).ToListAsync();
            var selectedQuestions = new List<QuestionVm>();

            Random random = new Random();

            // Ensure that the number of questions to select is not more than the available questions
            int numberOfQuestionsToSelect = Math.Min(quiz.NoOfQuestions, questions.Count);

            while (selectedQuestions.Count < numberOfQuestionsToSelect)
            {
                // Generate a random index within the range of available questions
                int randomIndex = random.Next(0, questions.Count);

                // Check if the question is not already selected
                if (!selectedQuestions.Any(q => q.QId == questions[randomIndex].QId))
                {
                    // Add the selected question to the result list
                    selectedQuestions.Add(new QuestionVm
                    {
                        QId = questions[randomIndex].QId,
                        QText = questions[randomIndex].QText,
                        Ans1 = questions[randomIndex].Ans1,
                        Ans2 = questions[randomIndex].Ans2,
                        Ans3 = questions[randomIndex].Ans3,
                        Ans4 = questions[randomIndex].Ans4,
                        CorrectAns = questions[randomIndex].CorrectAns,
                        Category = questions[randomIndex].Category.Name,
                    });
                }
            }

            return selectedQuestions;

        }


    }
}
