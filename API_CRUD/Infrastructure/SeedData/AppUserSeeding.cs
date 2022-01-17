using API_CRUD.Model.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CRUD.Infrastructure.SeedData
{
    public class AppUserSeeding : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasData(
                new AppUser { Id = 1, UserName = "beast", Password = "123" },
                new AppUser { Id = 2, UserName = "savage", Password = "123" },
                new AppUser { Id = 3, UserName = "bear", Password = "123" });
        }
    }
}
