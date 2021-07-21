﻿using Lead2Change.Domain.Models;
using Lead2Change.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Services.Coaches
{
    public interface ICoachService
    {
        public Task<Coach> GetCoach(Guid id);
        public Task<Coach> Update(Coach coach);
    }
}
