using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Domain.Models;

namespace Lead2Change.Repositories.Goals
{
    public interface IGoalsRepository
    {
        public Task<List<Goal>> GetGoals(int take = 10, int skip = 0);

    }
}
