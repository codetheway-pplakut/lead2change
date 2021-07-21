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

        public async Task<Coach> GetCoach(Guid id)
        {
            return await AppDbContext.Coaches.FirstOrDefaultAsync(i => i.Id == id);
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

        public async Task<Coach> Delete(Coach model)
        {
            AppDbContext.Coaches.Remove(model);
            await AppDbContext.SaveChangesAsync();
            return model;
        }
        public async Task<Coach> GetCoach(Guid id)
        {
            return await AppDbContext.Coaches.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Coach> Update(Coach model)
        {
            var result = _appDbContext.Coaches.Update(model);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}