using InfraStructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Interfaces
{
    public interface IAdminRepository
    {
        Task<ListPagerVm<AdminVm>> AdminsList(int currentPage);
        Task<bool> AdminCreate(AdminCreateVm model);
        Task<bool> IsNameExist(string name);
        Task<bool> AdminDelete(int id);
    }
}
