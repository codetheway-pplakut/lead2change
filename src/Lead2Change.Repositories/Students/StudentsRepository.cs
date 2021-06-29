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
    }
}
