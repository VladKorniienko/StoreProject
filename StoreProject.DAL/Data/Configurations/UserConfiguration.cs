using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.DAL.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Balance)
                .HasColumnType("decimal(18,2)");
            builder.Property(u => u.UserName)
                .IsRequired();
            builder.HasIndex(u => u.UserName)
                .IsUnique();
            builder.Property(u => u.Email)
                .IsRequired();
            builder.HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
