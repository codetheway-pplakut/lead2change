using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lead2Change.Data.Contexts;
using Lead2Change.Domain.Models;

namespace Lead2Change.Repositories.Goals
{
    public class GoalsRepository : _BaseRepository, IGoalsRepository
    {
        public GoalsRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<List<Goal>> GetGoals(int take = 10, int skip = 0)
        {
            return await _appDbContext.Goals.Take(take + skip).Skip(skip).ToListAsync();
        }

    }
}
