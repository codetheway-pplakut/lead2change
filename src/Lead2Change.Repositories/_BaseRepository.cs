using Lead2Change.Data.Contexts;
using Lead2Change.Domain.Constants;
using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Repositories
{
    public class _BaseRepository
    {
        protected AppDbContext _appDbContext;

        public _BaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        protected async Task Save()
        {
            await _appDbContext.SaveChangesAsync();
        }

        protected DbResponse<T> Success<T>(T model, string message)
        {
            return LogEvent<T>(model, StringConstants.SUCCESS, message);
        }

        protected DbResponse<T> Error<T>(T model, string message)
        {
            return LogEvent<T>(model, StringConstants.ERROR, message);
        }

        protected DbResponse<T> LogEvent<T>(T model, string status, string message)
        {
            return new DbResponse<T>()
            {
                Entity = model,
                Status = status,
                Message = message
            };
        }
    }
}
