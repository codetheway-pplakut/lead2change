using Lead2Change.Data.Contexts;
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

        public async Task<IdentityUser> GetUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityUser> GetUserByUserName(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
