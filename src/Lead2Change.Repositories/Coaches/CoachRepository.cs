using Lead2Change.Data.Contexts;
using Lead2Change.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Repositories.Coaches
{
    public class CoachRepository : _BaseRepository, ICoachRepository
    {
        private AppDbContext AppDbContext;
        public CoachRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.AppDbContext = dbContext;
        }
        public async Task<List<Coach>> GetCoaches()
        {
            return await AppDbContext.Coaches.ToListAsync();
        }
    }
}
