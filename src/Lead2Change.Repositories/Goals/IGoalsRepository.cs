using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lead2Change.Domain.Models;

namespace Lead2Change.Repositories.Goals
{
    public interface IGoalsRepository
    {
        public Task<Goal> Create(Goal goal);
    }
}
