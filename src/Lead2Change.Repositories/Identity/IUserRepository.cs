using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Repositories.Identity
{
    public interface IUserRepository
    {
        public Task<IdentityUser> GetUserById(Guid id);
        public Task<IdentityUser> GetUserByUserName(string userName);
    }
}
