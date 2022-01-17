using API_CRUD.Model.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CRUD.Infrastructure.SeedData
{
    public class CategorySeeding : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category { Id = 1, Name = "Boxing Gloves", Description = "Best boxing gloves are Everlast..!", Slug = "boxing-gloves" },
                new Category { Id = 2, Name = "Protective Equipment", Description = "Best protective equipment are Everlast..!", Slug = "protective-equipment" },
                new Category { Id = 3, Name = "Head Gear", Description = "Best head gear are Everlast..!", Slug = "head-gear" });
        }
    }
}
