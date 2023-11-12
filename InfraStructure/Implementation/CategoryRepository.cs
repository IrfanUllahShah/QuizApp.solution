using DomainModels;
using InfraStructure.Interfaces;
using InfraStructure.Pagination;
using InfraStructure.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly QuizAppContext db;
        private readonly IGenericRepository _genericRepository;

        public CategoryRepository(QuizAppContext quizAppContext,IGenericRepository genericRepository)
        {
            db = quizAppContext;
            _genericRepository = genericRepository;
        }

        //Category list for dropdown
        public async Task<IEnumerable<CategoryVm>> Categories()
        {
            
            var Categories = await _genericRepository.GetAll<Category>();
            var result = new List<CategoryVm>();
           
            if (Categories != null && Categories.Count > 0)
            {
                foreach (var item in Categories)
                {
                    result.Add(new CategoryVm()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                    });
                }
               
                return result;
            }
            return result;
        }


        //Category list with pagination
        public async Task<ListPagerVm<CategoryVm>> CategoriesList(int currentPage)
        {
            var listPager = new ListPagerVm<CategoryVm>();  //make object of viewModel

            // Apply pagination on records
            var allItems = await _genericRepository.GetAll<Category>();
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
            var result = new List<CategoryVm>();
            var Categories = allItems.Skip(recordsSkip).Take(pager.PageSize).ToList();
            if (Categories != null && Categories.Count > 0)
            {
                foreach (var item in Categories)
                {
                    result.Add(new CategoryVm()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                    });
                }
                listPager.list = result;
                return listPager;
            }
            return listPager;
        }

        //Create Category
        public async Task<bool> CategoryCreate(CategoryCreateVm model)
        {
            var category = new Category()
            {
                Name = model.Name,
                Description = model.Description,
            };
            await _genericRepository.Create<Category>(category);
            return true;

        }

        //Delete Category
        public async Task<bool> CategoryDelete(int id)
        {
            var category = await db.Categories.Where(p => p.Id == id).FirstOrDefaultAsync();
            _genericRepository.Delete<Category>(category);
            return true;
        }

        //Get by id Category
        public async Task<CategoryUpdateVm> CategoryGetById(int id)
        {
            var category = await db.Categories.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (category != null)
            {
                return new CategoryUpdateVm()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                };
            }
            else
            {
                return null;
            }
        }

        //update Category
        public async Task<bool> CategoryUpdate(CategoryUpdateVm model)
        {
            var category = db.Categories.Where(p => p.Id == model.Id).FirstOrDefault();
            if (category != null)
            {
                category.Id = model.Id;
                category.Name = model.Name;
                category.Description = model.Description;

            }
            _genericRepository.Update<Category>(category);
            return true;
        }

        // Category questions
        public async Task<IEnumerable<QuestionVm>> CategoryQuestions(int id)
        {
            var result = new List<QuestionVm>();
            var questions = await db.Questions.Include(p => p.Category).Where(p => p.CategoryId == id).ToListAsync();
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
                return result;
            }
            return result;
        }

        //Delete category Question
        public async Task<bool> CategoryQuestionDelete(int id)
        {
                var question = await db.Questions.Where(p => p.QId == id).FirstOrDefaultAsync();
                _genericRepository.Delete<Question>(question);
                return true;
        }




    }
}
