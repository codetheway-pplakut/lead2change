using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lead2Change.Services.Goals
{
    public interface IGoalsService
    {
        public Task<List<Goal>> GetGoals();
        public Task<Goal> GetGoal(Guid id);
        public Task<Goal> Update(Goal model);
    }
}
