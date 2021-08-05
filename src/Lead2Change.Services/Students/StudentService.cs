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
        public async Task<List<Student>> GetActiveStudents()
        {
            return await this._studentRepo.GetActiveStudents();
        }
        public async Task<List<Student>> GetActiveStudentsByPage(int pageNumber, int pageLength)
        {
            return await this._studentRepo.GetActiveStudentsByPage(pageNumber, pageLength);
        }
        public async Task<List<Student>> GetInactiveStudents()
        {
            return await this._studentRepo.GetInactiveStudents();
        }
        public async Task<Student> Update(Student student)
        {
            return await this._studentRepo.Update(student);
        }
        public async Task<Student> Create(Student student)
        {
            return await this._studentRepo.Create(student);
        }
        public bool HasCareerAssosiation(Student student)
        {
            return student.CareerDeclarationId != Guid.Empty;
        }
        public async Task<List<Student>> GetUnassignedStudents()
        {
            return await this._studentRepo.GetUnassignedStudents();
        }

        public async Task<List<Student>> GetCoachStudents(Guid coachId)
        {
            return await this._studentRepo.GetCoachStudents(coachId);
        }
    }
}