using InfraStructure.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Interfaces
{
    public interface ITeacherRepository
    {
        Task<ListPagerVm<TeacherVm>> TeachersList(int currentPage);
        Task<bool> IsImageFile(IFormFile file);
        Task<bool> TeacherCreate(TeacherCreateVm model);
        Task<bool> TeacherDelete(int id);
        Task<TeacherUpdateVm> TeacherGetById(int id);
        Task<bool> TeacherUpdate(TeacherUpdateVm model);
        Task<TeacherVm> TeacherDetails(int id);
    }
}
