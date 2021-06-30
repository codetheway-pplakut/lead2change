using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Repositories.Students
{
    public interface IStudentsRepository
    {

        public Task<List<Student>> GetStudents();
        public Task<Student> GetStudent(Guid id);
        public Task<Student> Delete(Student model);
        public Task<Student> Create(Student student);
    }
}
