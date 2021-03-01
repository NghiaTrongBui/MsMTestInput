using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MsM_Test.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MsM_Test.Data.EntityFrameworkCore
{
    public class MsM_TestDbcontext : DbContext
    {
        public MsM_TestDbcontext( DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
 
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });

            /*
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);
            */

            base.OnModelCreating(modelBuilder);
        }

        // public DbSet<AppConfig> AppConfigs { set; get; }

    }
}
