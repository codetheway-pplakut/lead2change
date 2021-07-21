using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Lead2Change.Repositories.Coaches
{
    public interface ICoachesRepository
    {
        public Task<Coach> Update(Coach student);
        public Task<Coach> GetCoach(Guid id);
    }
}
