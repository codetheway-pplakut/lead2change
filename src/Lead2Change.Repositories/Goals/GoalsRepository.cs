using System;
using System.Collections.Generic;
using Lead2Change.Domain.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Lead2Change.Data.Contexts;

namespace Lead2Change.Repositories.Goals
{
    public class GoalsRepository : IGoalsRepository
    {
        private AppDbContext AppDbContext;

        public GoalsRepository(AppDbContext dbContext)
        {
            this.AppDbContext = dbContext;
        }
        public async Task<List<Goal>> GetGoals()
        {
            return await this.AppDbContext.Goals.ToListAsync();
        }
        public async Task<Goal> GetGoals(Guid id)
        {
            return await AppDbContext.Goals.FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
