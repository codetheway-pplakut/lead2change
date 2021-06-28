using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Services.AppEvents
{
    public interface IAppEventService
    {
        public Task<AppEvent> LogEvent(AppEvent appEvent);
    }
}
