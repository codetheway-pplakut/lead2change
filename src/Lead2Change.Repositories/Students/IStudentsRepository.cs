using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Repositories.Students
{
    public interface IStudentsRepository
    {
        public Task<DbResponse<Student>> Create(Student student);
        public Task<DbResponse<Student>> Update(Student student);
        public Task<Student> Read(Guid id);
        public Task<List<Student>> GetStudents(int take = 10, int skip = 0);
        public Task<DbResponse<Student>> Delete(Student student);
    }
}
