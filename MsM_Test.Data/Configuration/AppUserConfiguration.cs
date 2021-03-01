using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsM_Test.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MsM_Test.Data.Configuration
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(64);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(64);
            builder.Property(x => x.Dob);
        }
    }
}
