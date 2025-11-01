using kTracker.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kTracker.Api.Core.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Shift> Shifts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(user => user.Id);

                entity.HasIndex(user => user.Username)
                      .IsUnique();

                entity.Property(user => user.Username)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(user => user.PasswordHash)
                      .IsRequired();

                entity.Property(user => user.Role)
                      .HasConversion<string>()
                      .HasMaxLength(10)
                      .IsRequired();

                entity.HasOne(u => u.Employee)
                      .WithOne(e => e.User)
                      .HasForeignKey<Employee>(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.UserId)
                      .IsUnique();

                entity.Property(e => e.Name)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(e => e.Surname)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(e => e.Patronymic)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(e => e.JobTitle)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(e => e.Latitude)
                      .IsRequired()
                      .HasColumnType("double precision");

                entity.Property(e => e.Longitude)
                      .IsRequired()
                      .HasColumnType("double precision");

                entity.Property(s => s.UpdateTime)
                      .HasColumnType("timestamp with time zone");

                entity.Property(e => e.RefreshToken)
                      .HasMaxLength(500);

                entity.HasMany(e => e.Shifts)
                      .WithOne(s => s.Employee)
                      .HasForeignKey(s => s.EmployeeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.HasIndex(s => s.EmployeeId);

                entity.Property(s => s.StartTime)
                      .IsRequired()
                      .HasColumnType("timestamp with time zone");

                entity.Property(s => s.EndTime)
                      .IsRequired()
                      .HasColumnType("timestamp with time zone");

                entity.HasIndex(s => s.StartTime);
                entity.HasIndex(s => s.EndTime);

                entity.HasIndex(s => new { s.StartTime, s.EndTime });
            });
        }
    }
}
