using Lead2Change.Data.Contexts;
using Lead2Change.Domain.Constants;
using Lead2Change.Domain.Models;
using Lead2Change.Domain.ViewModels;
using Lead2Change.Repositories.AppEvents;
using Lead2Change.Repositories.Coach;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Services.Coach
{
    public class CoachService : _BaseService, IStudentService
    {
        ICoachRepository _coachRepo;

        public CoachService(AppDbContext dbContext) : base(dbContext)
        {
            _coachRepo = new CoachRepository(dbContext);
        }

        public async Task<Coach> Create(Coach coach)
        {
            return await this._coachRepo.Create(coach);
        }
    }
}
