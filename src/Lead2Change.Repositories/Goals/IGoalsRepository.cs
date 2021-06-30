using System;
using System.Collections.Generic;
using System.Text;
using Lead2Change.Domain.Models;
using System.Threading.Tasks;
using System.Linq;

namespace Lead2Change.Repositories.Goals
{
    public interface IGoalsRepository
    {
        public Task<List<Goal>> GetGoals();
        public Task<Goal> GetGoals(Guid id);
    }
}
