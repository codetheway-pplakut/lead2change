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
        public Task<Goal> Create(Goal goal);
        public Task<List<Goal>> GetGoals();
    }
}
