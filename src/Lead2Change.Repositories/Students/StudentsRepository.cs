using Lead2Change.Data.Contexts;
using Lead2Change.Domain.Constants;
using Lead2Change.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Repositories.Students
{
    public class StudentsRepository : _BaseRepository, IStudentsRepository
    {
        private AppDbContext AppDbContext;
        public StudentsRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.AppDbContext = dbContext;
        }

        public async Task<List<Student>> GetStudents()
        {
            return await AppDbContext.Students.ToListAsync();
        }
        public async Task<Student> GetStudent(Guid id)
        {
            return await AppDbContext.Students.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Student> Read(Guid id)
        {
            return await _appDbContext.Students.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Student> Delete(Student model)
        {
            AppDbContext.Students.Remove(model);
            await AppDbContext.SaveChangesAsync();
            return model;
        }
        public async Task<Student> Update(Student model)
        {
            var result = _appDbContext.Students.Update(model);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<Student> Create(Student student)
        {
            var result = await this.AppDbContext.AddAsync(student);
            await this.AppDbContext.SaveChangesAsync();

            return result.Entity;
        }
    }
}
