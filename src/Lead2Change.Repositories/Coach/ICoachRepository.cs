using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Repositories.Coach
{
    interface ICoachRepository
    {
        public Task<Coach> Create(Coach coach);
    }
}
