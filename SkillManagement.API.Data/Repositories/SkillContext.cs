using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkillManagement.API.Core.Models;

namespace SkillManagement.API.Data.Repositories
{
    public class SkillContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public DbSet<Level> Levels { get; set; }

        public DbSet<UserSkillLevel> UserSkillLevels { get; set; }
        public SkillContext(DbContextOptions<SkillContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(p => p.FirstName).IsRequired().HasMaxLength(100);
            builder.Entity<User>().Property(p => p.LastName).HasMaxLength(100);
            builder.Entity<User>().Property(p => p.EmailId).HasMaxLength(100);
            

            builder.Entity<Skill>().ToTable("Skills");
            builder.Entity<Skill>().HasKey(p => p.Id);
            builder.Entity<Skill>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Skill>().Property(p => p.SkillName).IsRequired().HasMaxLength(100);
            builder.Entity<Skill>().Property(p => p.Description).HasMaxLength(100);

            builder.Entity<Level>().ToTable("Levels");
            builder.Entity<Level>().HasKey(p => p.Id);
            builder.Entity<Level>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Level>().Property(p => p.LevelName).IsRequired().HasMaxLength(100);
            builder.Entity<Level>().Property(p => p.Description).HasMaxLength(100);

            builder.Entity<UserSkillLevel>().ToTable("UserSkillLevel");
            builder.Entity<UserSkillLevel>().HasKey(p => p.Id);
            //builder.Entity<UserSkillLevel>().HasKey(p => new { p.UserId, p.SkillId, p.LevelId });
            builder.Entity<UserSkillLevel>().HasOne<User>(p => p.User).WithMany(p => p.UserSkillLevel).HasForeignKey(p => p.UserId);
            builder.Entity<UserSkillLevel>().HasOne<Skill>(p => p.Skill).WithMany(p => p.UserSkillLevel).HasForeignKey(p => p.SkillId);
            builder.Entity<UserSkillLevel>().HasOne<Level>(p => p.Level).WithMany(p => p.UserSkillLevel).HasForeignKey(p => p.LevelId);
        }


        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntityClass && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntityClass)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntityClass)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }



    }

}