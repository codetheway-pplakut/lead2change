using Lead2Change.Data.Contexts;
using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Repositories.AppEvents
{
    public class AppEventsRepository : _BaseRepository, IAppEventsRepository
    {
        public AppEventsRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }

        public async Task<AppEvent> Create(AppEvent appEvent)
        {
            var response = await _appDbContext.AppEvents.AddAsync(appEvent);
            return response.Entity;
        }
    }
}
