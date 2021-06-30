using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Domain.Models;
using Lead2Change.Repositories.Goals;
using Lead2Change.Data.Contexts;
using Lead2Change.Repositories;
using Lead2Change.Repositories.Goals;
using Lead2Change.Domain.Models;
using Lead2Change.Data.Contexts;
using System.Threading.Tasks;


namespace Lead2Change.Services.Goals
{

    
    public class GoalsService : IGoalsService
    {
        private IGoalsRepository GoalsRepository;

            public GoalsService(AppDbContext dbContext)
        {
            this.GoalRepo = new GoalsRepository(dbContext);
        }

        public async Task<Goal> Create(Goal goal)
        {
            return await this.GoalRepo.Create(goal);
        }
        public GoalsService(AppDbContext dbContext)
        {
            this.GoalsRepository = new GoalsRepository(dbContext);
        }
        public async Task<List<Goal>> GetGoals()
        {
            return await this.GoalsRepository.GetGoals();
        }

    }
}
