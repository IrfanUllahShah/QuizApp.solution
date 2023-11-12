using InfraStructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Interfaces
{
    public interface IQuizRepository
    {
        Task<IEnumerable<SetExamVm>> Examslist(); //exam list for dropdown
        Task<ListPagerVm<SetExamVm>> Exams(int currentPage);   //exams list with pagination for view
        Task<(bool Success, int QuestionsCount)> SetExam(SetExamCreateVm item);
        Task<bool> ExamDelete(int id);
        Task<List<string>> Results();
        Task<ListPagerVm<ResultVm>> ExamGetByName(string examName, int currentPage);
    }
}
