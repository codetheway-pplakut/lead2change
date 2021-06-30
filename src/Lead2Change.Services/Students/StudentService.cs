using Lead2Change.Data.Contexts;
using Lead2Change.Domain.Constants;
using Lead2Change.Domain.Models;
using Lead2Change.Domain.ViewModels;
using Lead2Change.Repositories.AppEvents;
using Lead2Change.Repositories.Students;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Services.Students
{
    public class StudentService : _BaseService, IStudentService
    {
        IStudentsRepository _studentRepo;

        public StudentService(AppDbContext dbContext) : base(dbContext)
        {
            _studentRepo = new StudentsRepository(dbContext);
        }

        public async Task<List<Student>> GetStudents(int take, int skip)
        {
            return await _studentRepo.GetStudents(take, skip);
        }

        /// <summary>
        /// EXAMPLE: THIS IS NOT A REAL METHOD
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public async Task<List<Student>> GetSomeStudents(int take, int skip)
        {
            //If we have 1000 Students in our DB, this query will allow us to take 100 of those Students. In this instance, it will take Students 50-150.
            //The first parameter "100" specifies how many records we want to take.
            //The second parameter "50" defines the number of records to skip. 
            return await _studentRepo.GetStudents(100, 50);
        }
    }
}