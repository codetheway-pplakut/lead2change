using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Repositories.Coaches
{
    public interface ICoachesRepository
    {
        public Task<Coach> Create(Coach coach);
        public Task<List<Coach>> GetCoaches();
        public Task<Coach> Update(Coach student);
        public Task<Coach> Delete(Coach model);
        public Task<Coach> GetCoach(Guid id);
    }
    }