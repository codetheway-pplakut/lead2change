using Lead2Change.Data.Contexts;
using Lead2Change.Domain.Models;
using Lead2Change.Repositories.Coaches;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Services.Coaches
{
    public class CoachService : _BaseService, ICoachService
    {
        ICoachRepository _coachRepo;
        public CoachService(AppDbContext dbContext) : base(dbContext)
        {
            _coachRepo = new CoachRepository(dbContext);
        }
        public async Task<List<Coach>> GetCoaches()
        {
            return await this._coachRepo.GetCoaches();
        }
    }
}
