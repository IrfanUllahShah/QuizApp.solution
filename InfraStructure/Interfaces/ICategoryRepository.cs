using InfraStructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryVm>> Categories();  //for dropdown
        Task<ListPagerVm<CategoryVm>> CategoriesList(int currentPage); //list with pagination for view
        Task<bool> CategoryCreate(CategoryCreateVm model);
        Task<bool> CategoryDelete(int id);
        Task<CategoryUpdateVm> CategoryGetById(int id);
        Task<bool> CategoryUpdate(CategoryUpdateVm model);
        Task<IEnumerable<QuestionVm>> CategoryQuestions(int id);
        Task<bool> CategoryQuestionDelete(int id);
    }
}
