using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Lead2Change.Domain.Models;
using Lead2Change.Data.Contexts;
using System.Threading.Tasks;

namespace Lead2Change.Repositories.Goals
{
    public class GoalsRepository : IGoalsRepository
    {
        private AppDbContext AppDbContext;
        public GoalsRepository(AppDbContext dbContext)
        {
            this.AppDbContext = dbContext;
        }
        public async Task<Goal> Create(Goal goal)
        {
            var result = await this.AppDbContext.AddAsync(goal);
            await this.AppDbContext.SaveChangesAsync();

            return result.Entity;
        }
    }
}
