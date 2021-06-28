using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Repositories.AppEvents
{
    public interface IAppEventsRepository
    {
        public Task<AppEvent> Create(AppEvent appEvent);
    }
}
