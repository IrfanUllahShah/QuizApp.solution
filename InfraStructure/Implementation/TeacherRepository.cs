using DomainModels;
using InfraStructure.Interfaces;
using InfraStructure.Pagination;
using InfraStructure.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Implementation
{
    public class TeacherRepository: ITeacherRepository
    {
        private readonly QuizAppContext db;
        private readonly IGenericRepository _genericRepository;

        public TeacherRepository(QuizAppContext quizAppContext, IGenericRepository genericRepository)
        {
            db = quizAppContext;
            _genericRepository = genericRepository;
        }

        //Teachers list
        public async Task<ListPagerVm<TeacherVm>> TeachersList(int currentPage)
        {
            var listPager = new ListPagerVm<TeacherVm>();  //make object of viewModel

            // Apply pagination on records
            var allItems = await _genericRepository.GetAll<Teacher>();
            const int pageSize = 3;
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
            var result = new List<TeacherVm>();
            var teachers = allItems.Skip(recordsSkip).Take(pager.PageSize).ToList();
            if (teachers != null && teachers.Count > 0)
            {
                foreach (var item in teachers)
                {
                    result.Add(new TeacherVm()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Username = item.Username,
                        Password = item.Password,
                        Image = item.Image,
                        //Image=item.Image,
                    });
                }
                listPager.list = result;
                return listPager;
            }
            return listPager;
        }

        //method for checking image file
        public async Task<bool> IsImageFile(IFormFile file)
        {
            if (file == null)
            {
                return false;
            }

            // Check if the file has a valid image extension
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".ico", ".jfif", ".webp" };
            string extension = Path.GetExtension(file.FileName).ToLower();

            return allowedExtensions.Contains(extension);
        }

        //Create Teachers
        public async Task<bool> TeacherCreate(TeacherCreateVm model)
        {
            var teacher = new Teacher()
            {
                Name = model.Name,
                Password = model.Password,
                Username = model.Username,
                Image = model.Image,
            };
            await _genericRepository.Create<Teacher>(teacher);
            return true;

        }

        //Delete Teachers
        public async Task<bool> TeacherDelete(int id)
        {
            var teacher = await db.Teachers.Where(p => p.Id == id).FirstOrDefaultAsync();
           _genericRepository.Delete<Teacher>(teacher);
            return true;
        }

        //Get by id Teacher
        public async Task<TeacherUpdateVm> TeacherGetById(int id)
        {
            var teacher = await db.Teachers.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (teacher != null)
            {
                return new TeacherUpdateVm()
                {
                    Id = teacher.Id,
                    Name = teacher.Name,
                    Password = teacher.Password,
                    Username = teacher.Username,
                    Image = teacher.Image,
                };
            }
            else
            {
                return null;
            }
        }

        //update Teachers
        public async Task<bool> TeacherUpdate(TeacherUpdateVm model)
        {
            var teacher = await db.Teachers.Where(p => p.Id == model.Id).FirstOrDefaultAsync();
            if (teacher != null)
            {
                teacher.Id = model.Id;
                teacher.Name = model.Name;
                teacher.Password = model.Password;
                teacher.Username = model.Username;
                teacher.Image = model.Image;

            }
            _genericRepository.Update<Teacher>(teacher);
            return true;
        }

        //details Teacher
        public async Task<TeacherVm> TeacherDetails(int id)
        {
            var teacher = await db.Teachers.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (teacher != null)
            {
                return new TeacherVm()
                {
                    Id = teacher.Id,
                    Name = teacher.Name,
                    Password = teacher.Password,
                    Username = teacher.Username,
                    Image = teacher.Image,
                };
            }
            else
            {
                return null;
            }
        }

    }
}
