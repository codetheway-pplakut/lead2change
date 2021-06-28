using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Services.Identity
{
    public interface IIdentityService
    {
        public Task<IdentityUser> GetUserById(Guid id);
        public Task<IdentityUser> GetUserByUserName(string userName);
    }
}
