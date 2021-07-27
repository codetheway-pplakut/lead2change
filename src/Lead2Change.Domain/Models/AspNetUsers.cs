using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Lead2Change.Domain.Models
{
    public partial class AspNetUsers : IdentityUser
    {
        public Guid StudentId { get; set; }

        //public AspNetUsers()
        //{
        //    AspNetUserClaims = new HashSet<AspNetUserClaims>();
        //    AspNetUserLogins = new HashSet<AspNetUserLogins>();
        //    AspNetUserRoles = new HashSet<AspNetUserRoles>();
        //    AspNetUserTokens = new HashSet<AspNetUserTokens>();
        //}

        //public Guid Id { get; set; }
        //public string UserName { get; set; }
        //public string NormalizedUserName { get; set; }
        //public string Email { get; set; }
        //public string NormalizedEmail { get; set; }
        //public long EmailConfirmed { get; set; }
        //public string PasswordHash { get; set; }
        //public string SecurityStamp { get; set; }
        //public string ConcurrencyStamp { get; set; }
        //public string PhoneNumber { get; set; }
        //public long PhoneNumberConfirmed { get; set; }
        //public long TwoFactorEnabled { get; set; }
        //public string LockoutEnd { get; set; }
        //public long LockoutEnabled { get; set; }
        //public long AccessFailedCount { get; set; }

        //public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        //public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        //public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        //public virtual ICollection<AspNetUserTokens> AspNetUserTokens { get; set; }
    }
}
