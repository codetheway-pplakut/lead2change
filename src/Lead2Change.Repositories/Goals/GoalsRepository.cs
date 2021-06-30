using System;
using System.Collections.Generic;
using System.Text;
using Lead2Change.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<Goal> GetGoal(Guid id) {
            return await this.AppDbContext.Goals.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Goal> Update(Goal model)
        {
            var result = AppDbContext.Goals.Update(model);
            await AppDbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
