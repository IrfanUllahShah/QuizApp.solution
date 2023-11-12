using DomainModels;
using InfraStructure.Interfaces;
using InfraStructure.Pagination;
using InfraStructure.SecurityAcids;
using InfraStructure.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Implementation
{
    public class QuizRepository: IQuizRepository
    {
        private readonly QuizAppContext db;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IGenericRepository _genericRepository;

        public QuizRepository(QuizAppContext quizAppContext, ICategoryRepository categoryRepository,IGenericRepository genericRepository)
        {
            db = quizAppContext;
            _categoryRepository = categoryRepository;
            _genericRepository = genericRepository;
        }

        // Exams list for dropdown
        public async Task<IEnumerable<SetExamVm>> Examslist()
        {
            var exams = await db.SetExams.Include(p => p.Category).OrderByDescending(p => p.ExamDate).ToListAsync();
          
            var result = new List<SetExamVm>();

            if (exams != null && exams.Count > 0)
            {
                foreach (var item in exams)
                {
                    result.Add(new SetExamVm()
                    {
                        Examid = item.Examid,
                        ExamName = item.ExamName,
                        RoomCode = item.RoomCode,
                        ExamDate = item.ExamDate,
                        NoOfQuestions = item.NoOfQuestions,
                        Category = item.Category.Name,
                    });
                }
                return result;
            }
            return result;
        }


        // Exams list with pagination
        public async Task<ListPagerVm<SetExamVm>> Exams(int currentPage)
        {
            var listPager = new ListPagerVm<SetExamVm>();  //make object of viewModel

            // Apply pagination on records
            var allItems = await db.SetExams.Include(p => p.Category).ToListAsync();
            const int pageSize = 5;
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
            var result = new List<SetExamVm>();
            var exams = allItems.Skip(recordsSkip).Take(pager.PageSize).ToList();
          
            if (exams != null && exams.Count > 0)
            {
                foreach (var item in exams)
                {
                    result.Add(new SetExamVm()
                    {
                        Examid=item.Examid,
                        ExamName=item.ExamName,
                        RoomCode=item.RoomCode,
                        ExamDate=item.ExamDate,
                        NoOfQuestions=item.NoOfQuestions,
                        Category=item.Category.Name,
                    });
                }
                listPager.list = result;
                return listPager;
            }
            return listPager;
        }

        // Exams Create
        public async Task<(bool Success, int QuestionsCount)> SetExam(SetExamCreateVm item)
        {
            var result = new List<QuestionVm>();
            var questions = await db.Questions.Include(p => p.Category).Where(p => p.CategoryId == item.CategoryId).ToListAsync();
            if (item.NoOfQuestions > questions.Count)
            {
                return (false, questions.Count);
            }
            var exam = new SetExam()
            {
              ExamName=item.ExamName,
              NoOfQuestions=item.NoOfQuestions,
              ExamDate=DateTime.Now,
              RoomCode=EncryptionClass.EnryptString(item.ExamName),
              CategoryId = item.CategoryId,
            };
            await _genericRepository.Create<SetExam>(exam);
            return (true,0);
        }

        //Delete Exam
        public async Task<bool> ExamDelete(int id)
        {
            var exam = await db.SetExams.Where(p => p.Examid == id).FirstOrDefaultAsync();
            _genericRepository.Delete<SetExam>(exam);
            return true;
        }

        //Results
        public async Task<List<string>> Results()
        {
            var names = await db.SetExams.Select(exam => exam.ExamName).ToListAsync();

            return names;
        }

        //Quiz get by name and return result of students
        public async Task<ListPagerVm<ResultVm>> ExamGetByName(string examName,int currentPage)
        {
            //retrieve exam from name
            var exam = await db.SetExams.Where(p => p.ExamName== examName).FirstOrDefaultAsync();

            var listPager = new ListPagerVm<ResultVm>();  //make object of viewModel

            // Apply pagination on records
            var allItems = await db.Marks.Where(p => p.ExamId == exam.Examid).Include(p => p.Student).Include(p => p.Exam).ToListAsync();
            const int pageSize = 5;
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
            var result = new List<ResultVm>();
            var marks = allItems.Skip(recordsSkip).Take(pager.PageSize).ToList();

            if (marks != null && marks.Count > 0)
            {
                foreach (var item in marks)
                {
                    result.Add(new ResultVm()
                    {
                      Name=item.Student.Name,
                      Marks=item.Marks,
                      TotalMarks=item.Exam.NoOfQuestions,
                     percentage = (item.Marks * 100) / item.Exam.NoOfQuestions,
                    });
                }
                listPager.list = result;
                return listPager;
            }
            return listPager;


        }
    }
}
