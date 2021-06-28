using Lead2Change.Data.Contexts;
using Lead2Change.Domain.Models;
using Lead2Change.Repositories.AppEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Services.AppEvents
{
    public class AppEventsService : IAppEventService
    {
        IAppEventsRepository _appEventsRepo;

        public AppEventsService(AppDbContext dbContext)
        {
            _appEventsRepo = new AppEventsRepository(dbContext);
        }

        public async Task<AppEvent> LogEvent(AppEvent appEvent)
        {
            return await _appEventsRepo.Create(appEvent);
        }
    }
}
