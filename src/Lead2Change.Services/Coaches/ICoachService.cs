using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lead2Change.Domain.Models;
using Lead2Change.Domain.ViewModels;


namespace Lead2Change.Services.Coaches
{
    public interface ICoachService
    {
        public Task<List<Coach>> GetCoaches();

    }
}
