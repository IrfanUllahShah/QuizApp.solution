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
    public class AdminRepository: IAdminRepository
    {
        private readonly QuizAppContext db;
        private readonly IGenericRepository _genericRepository;

        public AdminRepository(QuizAppContext quizAppContext, IGenericRepository genericRepository) 
        {
            db = quizAppContext;
            _genericRepository = genericRepository;
        }

        //Admins list
        public async Task<ListPagerVm<AdminVm>> AdminsList(int currentPage)
        {
            var listPager = new ListPagerVm<AdminVm>();  //make object of viewModel

            // Apply pagination on records
            var allItems = await _genericRepository.GetAll<Admin>();
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
            var result = new List<AdminVm>();
            var admins = allItems.Skip(recordsSkip).Take(pager.PageSize).ToList();
            if (admins != null && admins.Count > 0)
            {
                foreach (var item in admins)
                {
                    result.Add(new AdminVm()
                    {
                        Id = item.Id,
                        Username = item.Username,
                        Password = item.Password,
                    });
                }
                listPager.list = result;
                return listPager;
            }
            return listPager;
        }

        //Create admin

        public async Task<bool> AdminCreate(AdminCreateVm model)
        {
            var admin = new Admin()
            {
                Username = model.Username,
                Password = model.Password,
            };
           _genericRepository.Create<Admin>(admin);
            return true;

        }

        //Name existence action
        public async Task<bool> IsNameExist(string name)
        {
            return await db.Admins.AnyAsync(p => p.Username.Equals(name));
        }

        //Delete admin
        public async Task<bool> AdminDelete(int id)
        {
            var result = new List<AdminVm>();
            var admins = await _genericRepository.GetAll<Admin>();

            if (admins != null && admins.Count > 1)
            {
                var admin = await db.Admins.Where(p => p.Id == id).FirstOrDefaultAsync();
                _genericRepository.Delete<Admin>(admin);
                return true;
            }
            return false;
        }
    }
}
