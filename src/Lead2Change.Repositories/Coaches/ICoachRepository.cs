using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lead2Change.Domain.Models;



namespace Lead2Change.Repositories.Coaches
{
    public interface ICoachRepository
    {
        public Task<List<Coach>> GetCoaches();
    }
}
