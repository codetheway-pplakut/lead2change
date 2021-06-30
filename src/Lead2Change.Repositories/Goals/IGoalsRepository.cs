using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Domain.Models;
using System.Text;
using Lead2Change.Domain.Models;
using System.Threading.Tasks;
using System.Linq;
using Lead2Change.Data.Contexts;

namespace Lead2Change.Repositories.Goals
{
    public interface IGoalsRepository
    {
        public Task<List<Goal>> GetGoals();
        public Task<Goal> GetGoal(Guid id);
        public Task<Goal> Update(Goal model);
        public Task<Goal> Create(Goal goal);
    }
}
