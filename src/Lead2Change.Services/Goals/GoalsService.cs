using System;
using System.Collections.Generic;
using System.Text;
using Lead2Change.Repositories.Goals;
using Lead2Change.Data.Contexts;
using Lead2Change.Domain.Models;
using System.Threading.Tasks;

namespace Lead2Change.Services.Goals
{
    public class GoalsService : IGoalsService
    {
        private IGoalsRepository GoalsRepository;

        public GoalsService(AppDbContext dbContext)
        {
            this.GoalsRepository = new GoalsRepository(dbContext);
        }
        public async Task<Goal> GetGoal(Guid id)
        {
            return await this.GoalsRepository.GetGoal(id);
        }
        public async Task<Goal> Update(Goal model)
        {
            return await this.GoalsRepository.Update(model);
        }

    }
}
