using DomainModels;
using InfraStructure.Interfaces;
using InfraStructure.Pagination;
using InfraStructure.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Implementation
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly QuizAppContext db;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IGenericRepository _genericRepository;

        public QuestionRepository(QuizAppContext quizAppContext, ICategoryRepository categoryRepository,IGenericRepository genericRepository)
        {
            db = quizAppContext;
            _categoryRepository = categoryRepository;
            _genericRepository = genericRepository;
        }

        // questions list
        public async Task<ListPagerVm<QuestionVm>> Questions(int currentPage)
        {
            var listPager = new ListPagerVm<QuestionVm>();  //make object of viewModel

            // Apply pagination on records
            var allItems= await db.Questions.Include(p => p.Category).ToListAsync();
            const int pageSize = 5;
            if (currentPage < 1)
            {
                currentPage = 1;
            }
            int recordsCount=allItems.Count;

            // object of Pager class
            var pager=new Pager(recordsCount,currentPage,pageSize);

            //pager for sending to view
            listPager.pager = pager;

            //skip records
            int recordsSkip=(currentPage - 1)*pageSize;

            //select questions for view from all records
            var result = new List<QuestionVm>();
            var questions = allItems.Skip(recordsSkip).Take(pager.PageSize).ToList();
            if (questions != null && questions.Count > 0)
            {
                foreach (var item in questions)
                {
                    result.Add(new QuestionVm()
                    {
                        QId = item.QId,
                        QText = item.QText,
                        Ans1 = item.Ans1,
                        Ans2 = item.Ans2,
                        Ans3 = item.Ans3,
                        Ans4 = item.Ans4,
                        CorrectAns = item.CorrectAns,
                        Category = item.Category.Name,
                    });
                }
                listPager.list = result;
                return listPager;
            }
            return listPager;
        }

        //Create Question
        public async Task<bool> QuestionCreate(QuestionCreateVm item)
        {
            var question = new Question()
            {
                QText = item.QText,
                Ans1 = item.Ans1,
                Ans2 = item.Ans2,
                Ans3 = item.Ans3,
                Ans4 = item.Ans4,
                CorrectAns = item.CorrectAns,
                CategoryId = item.CategoryId,
            };
            await _genericRepository.Create<Question>(question);
            return true;
        }

        //Delete Question
        public async Task<bool> QuestionDelete(int id)
        {
            var question = await db.Questions.Where(p => p.QId == id).FirstOrDefaultAsync();
            _genericRepository.Delete<Question>(question);
            return true;

        }

        //Get by id Question
        public async Task<QuestionUpdateVm> QuestionGetById(int id)
        {
            var question = await db.Questions.Where(p => p.QId == id).FirstOrDefaultAsync();
            if (question != null)
            {
                return new QuestionUpdateVm()
                {
                    QId = question.QId,
                    QText = question.QText,
                    Ans1 = question.Ans1,
                    Ans2 = question.Ans2,
                    Ans3 = question.Ans3,
                    Ans4 = question.Ans4,
                    CorrectAns = question.CorrectAns,
                    CategoryId = (int)question.CategoryId,
                    Categories =await _categoryRepository.Categories()
                };
            }
            else
            {
                return null;
            }
        }

        //update Question
        public async Task<bool> QuestionUpdate(QuestionUpdateVm model)
        {
            var question = await db.Questions.Where(p => p.QId == model.QId).FirstOrDefaultAsync();
            if (question != null)
            {
                question.QId = model.QId;
                question.QText = model.QText;
                question.Ans1 = model.Ans1;
                question.Ans2 = model.Ans2;
                question.Ans3 = model.Ans3;
                question.Ans4 = model.Ans4;
                question.CorrectAns = model.CorrectAns;
                question.CategoryId = model.CategoryId;
            }
            _genericRepository.Update<Question>(question);
            return true;
        }



    }
}
