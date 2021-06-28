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
        public StudentsRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<DbResponse<Student>> Create(Student model)
        {
            try
            {
                var student = await _appDbContext.Students.AddAsync(model);
                await Save();

                return Success<Student>(student.Entity, StringConstants.SUCCESS);
            }
            catch (Exception ex)
            {
                return Error<Student>(model, ex.StackTrace);
            }
        }

        public async Task<DbResponse<Student>> Delete(Student model)
        {
            try
            {
                _appDbContext.Students.Remove(model);
                await Save();

                return Success<Student>(model, StringConstants.SUCCESS);
            }
            catch (Exception ex)
            {
                return Error<Student>(model, ex.StackTrace);
            }
        }

        public async Task<List<Student>> GetStudents(int take = 10, int skip = 0)
        {
            return await _appDbContext.Students.Take(take + skip).Skip(skip).ToListAsync();
        }

        public async Task<List<Student>> GetEnrolledStudents(int take = 10, int skip = 0)
        {
            return await _appDbContext.Students.Where(i => i.Enrolled).Take(take + skip).Skip(skip).ToListAsync();
        }

        public async Task<Student> Read(Guid id)
        {
            return await _appDbContext.Students.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<DbResponse<Student>> Update(Student model)
        {
            try
            {
                var student = _appDbContext.Students.Update(model);
                await Save();

                return Success<Student>(student.Entity, StringConstants.SUCCESS);
            }
            catch (Exception ex)
            {
                return Error<Student>(model, ex.StackTrace);
            }
        }
    }
}
