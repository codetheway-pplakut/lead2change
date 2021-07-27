using Lead2Change.Data.Contexts;
using Lead2Change.Domain.Models;
using Lead2Change.Repositories.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Services.Identity
{
    public class UserService : IUserService
    {
        IUserRepository _userRepo;

        public UserService(AppDbContext appDbContext)
        {
            _userRepo = new UserRepository(appDbContext);
        }

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
