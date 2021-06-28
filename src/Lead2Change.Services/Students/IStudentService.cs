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
        public Task<RegistrationViewModel> Register(RegistrationViewModel model);
        public Task<List<Student>> GetStudents(int take, int skip);
    }
}
