using Lead2Change.Data.Contexts;
using Lead2Change.Repositories.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        IUserRepository _userRepo;

        public IdentityService(AppDbContext appDbContext)
        {
            _userRepo = new UserRepository(appDbContext);
        }

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
