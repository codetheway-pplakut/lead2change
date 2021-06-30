using Lead2Change.Domain.Models;
using Lead2Change.Repositories.Goals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lead2Change.Services.Goals
{
    public class GoalsService : IGoalsService
    {
        private IGoalsRepository GoalsRepo;
        public async Task<List<Goal>> GetGoals()
        {
            return await this.GoalsRepo.GetGoals();
        }
        public async Task<Goal> GetGoals(Guid id)
        {
            return await this.GoalsRepo.GetGoals(id);
        }
    }
}
