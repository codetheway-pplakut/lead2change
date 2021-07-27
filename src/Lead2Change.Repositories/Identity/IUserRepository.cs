using Lead2Change.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Repositories.Identity
{
    public interface IUserRepository
    {
        public Task<AspNetUsers> GetUserById(Guid id);
        public Task<AspNetUsers> GetUserByUserName(string userName);
    }
}
