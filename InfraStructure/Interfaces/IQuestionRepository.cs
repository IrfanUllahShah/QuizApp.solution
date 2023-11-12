using InfraStructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Interfaces
{
    public interface IQuestionRepository
    {
        Task<ListPagerVm<QuestionVm>> Questions(int currentPage);
        Task<bool> QuestionCreate(QuestionCreateVm item);
        Task<bool> QuestionDelete(int id);
        Task<QuestionUpdateVm> QuestionGetById(int id);
        Task<bool> QuestionUpdate(QuestionUpdateVm model);
    }
}
