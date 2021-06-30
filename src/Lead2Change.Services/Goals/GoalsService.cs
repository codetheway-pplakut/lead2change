﻿using Lead2Change.Domain.Models;
using Lead2Change.Repositories.Goals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Domain.Models;
using Lead2Change.Repositories.Goals;
using Lead2Change.Data.Contexts;
using Lead2Change.Repositories;


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
        public async Task<Goal> Create(Goal goal)
        {
            return await this.GoalsRepository.Create(goal);
        }
        public async Task<List<Goal>> GetGoals()
        {
            return await this.GoalsRepository.GetGoals();

        }
    }
}
