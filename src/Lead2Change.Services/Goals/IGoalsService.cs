using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Domain.Models;

namespace Lead2Change.Services.Goals
{
    public interface IGoalsService
    {
        public Task<List<Goal>> GetGoals();
        public Task<Goal> GetGoal(Guid id);
        public Task<Goal> Update(Goal model);
        public Task<Goal> Create(Goal goal);
    }
}
