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
    }
}