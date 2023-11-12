using InfraStructure.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Interfaces
{
    public interface IStudentRepository
    {
        Task<ListPagerVm<StudentVm>> StudentsList(int currentPage);
        Task<bool> IsImageFile(IFormFile file);
        Task<bool> StudentCreate(StudentCreateVm model);
        Task<bool> StudentDelete(int id);
        Task<StudentUpdateVm> StudentGetById(int id);
        Task<bool> StudentUpdate(StudentUpdateVm model);
        Task<StudentVm> StudentDetails(int id);
        Task<IEnumerable<QuestionVm>> StartExam(string roomCode);


    }
}
