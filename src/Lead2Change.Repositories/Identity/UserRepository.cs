using Lead2Change.Data.Contexts;
using Lead2Change.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Repositories.Identity
{
    public class UserRepository : _BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<AspNetUsers> GetUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<AspNetUsers> GetUserByUserName(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
