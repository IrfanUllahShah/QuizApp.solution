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
        Task<IEnumerable<AdminVm>> AdminsList();
        Task<bool> AdminCreate(AdminCreateVm model);
        Task<bool> IsNameExist(string name);
    }
}
