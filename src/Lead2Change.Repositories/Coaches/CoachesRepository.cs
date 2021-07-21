using Lead2Change.Data.Contexts;
using Lead2Change.Domain.Constants;
using Lead2Change.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Repositories.Coaches
{
    public class CoachesRepository : _BaseRepository, ICoachesRepository
    {
        private AppDbContext AppDbContext;
        public CoachesRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.AppDbContext = dbContext;
        }

        public async Task<Coach> Create(Coach coach)
        {
            var result = await this.AppDbContext.AddAsync(coach);
            await this.AppDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<List<Coach>> GetCoaches()
        {
            return await AppDbContext.Coaches.ToListAsync();
    }
        }
}