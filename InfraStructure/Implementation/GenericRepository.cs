using DomainModels;
using InfraStructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Implementation
{
    public class GenericRepository: IGenericRepository
    {
        private readonly QuizAppContext db;

        public GenericRepository(QuizAppContext quizAppContext)
        {
            db = quizAppContext;
        }

        //Generic method for get all records from db
        public async Task<List<T>> GetAll<T>() where T : class
        {
            return await db.Set<T>().ToListAsync();
        }

        //Generic method for create record in db
        public async Task<bool> Create<T>(T entity) where T : class
        {
            await db.Set<T>().AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }

        //Generic method for update records in db
        public async Task<bool> Update<T>(T entity) where T : class
        {
            db.Set<T>().Update(entity);
            await db.SaveChangesAsync();
            return true;
        }

        //Generic method for delete records from db
        public async Task<bool> Delete<T>(T entity) where T : class
        {
            db.Set<T>().Remove(entity);
            await db.SaveChangesAsync();
            return true;
        }

    }
}
