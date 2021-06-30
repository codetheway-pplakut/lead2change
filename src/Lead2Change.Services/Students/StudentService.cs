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

        public async Task<Student> GetStudent(Guid id)
        {
            return await this._studentRepo.GetStudent(id);
        }
        public async Task<Student> Delete(Student student)
        {
            return await _studentRepo.Delete(student);
        }

        public async Task<List<Student>> GetStudents()
        {
            return await this._studentRepo.GetStudents();
        }
        public async Task<Student> Update(Student student)
        {
            return await this._studentRepo.Update(student);
        }
    }
}