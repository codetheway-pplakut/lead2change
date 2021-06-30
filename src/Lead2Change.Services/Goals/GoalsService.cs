﻿using Lead2Change.Domain.Models;
using Lead2Change.Repositories.Goals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Data.Contexts;

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

        public async Task<List<Goal>> GetGoals()
        {
            return await this.GoalsRepo.GetGoals();
        }
    }
}
