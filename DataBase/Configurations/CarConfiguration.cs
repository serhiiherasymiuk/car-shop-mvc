using DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configurations
{
    internal class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Model)
               .IsRequired()
               .HasMaxLength(100);
            builder.Property(c => c.Color)
               .IsRequired()
               .HasMaxLength(100);

            builder
                .HasOne(c => c.Brand)
                .WithMany(b => b.Cars)
                .HasForeignKey(c => c.BrandId);

            builder
                .HasMany(c => c.FavoriteUsers)
                .WithMany(u => u.FavoriteCars);

            builder
                .HasOne(c => c.User)
                .WithMany(u => u.CarsAdvertisements);
        }
    }
}
