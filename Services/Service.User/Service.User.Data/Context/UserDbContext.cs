using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Service.User.Domain;

namespace Service.User.Data
{
    public class UserDbContext: DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> option) : base(option)
        {
            
        }

        public DbSet<Account> Accounts { get; set; }
    }
}