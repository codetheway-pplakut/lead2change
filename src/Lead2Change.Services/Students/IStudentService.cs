using Lead2Change.Domain.Models;
using Lead2Change.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Services.Students
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudents();
        public Task<List<Student>> GetActiveStudents();
        public Task<List<Student>> GetInactiveStudents();
        public Task<Student> GetStudent(Guid id);
        public Task<Student> Delete(Student student);
        public Task<Student> Create(Student student);
        public Task<Student> Update(Student student);
        public bool HasCareerAssosiation(Student student);
        public Task<List<Student>> GetUnassignedStudents();
    }
}
