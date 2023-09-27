using DomainModels;
using InfraStructure.Interfaces;
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

        public AdminRepository(QuizAppContext quizAppContext) 
        {
            db = quizAppContext;
        }

        //Admins list
        public async Task<IEnumerable<AdminVm>> AdminsList()
        {
            var result = new List<AdminVm>();
            var admins = await db.Admins.ToListAsync();
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
                return result;
            }
            return result;
        }

        //Create admin

        public async Task<bool> AdminCreate(AdminCreateVm model)
        {
            var admin = new Admin()
            {
                Username = model.Username,
                Password = model.Password,
            };
            await db.Admins.AddAsync(admin);
            db.SaveChanges();
            return true;

        }

        //Name existence action
        public async Task<bool> IsNameExist(string name)
        {
            //if (db.Admins.Where(p => p.Username.Equals(name)).Any())
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return await db.Admins.AnyAsync(p => p.Username.Equals(name));
        }

        //Delete admin
        //public async Task<bool> AdminDelete(int id)
        //{
        //    var result = new List<AdminVm>();
        //    var entity = await db.Admins.Where(p => p.Id == id).FirstOrDefaultAsync();
        //}
    }
}
