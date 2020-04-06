using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SkillManagement.API.Models
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
            builder.Entity<UserSkillLevel>().HasKey(p => new { p.UserId, p.SkillId, p.LevelId });
            builder.Entity<UserSkillLevel>().HasOne<User>(p => p.User).WithMany(p => p.UserSkillLevel).HasForeignKey(p => p.UserId);
            builder.Entity<UserSkillLevel>().HasOne<Skill>(p => p.Skill).WithMany(p => p.UserSkillLevel).HasForeignKey(p => p.SkillId);
            builder.Entity<UserSkillLevel>().HasOne<Level>(p => p.Level).WithMany(p => p.UserSkillLevel).HasForeignKey(p => p.LevelId);
                       
            //var allEntities = builder.Model.GetEntityTypes();

            //foreach (var entity in allEntities)
            //{
            //    entity.AddProperty("CreatedDate", typeof(DateTime));
            //    entity.AddProperty("UpdatedDate", typeof(DateTime));
            //}

            builder.Entity<Level>().HasData(new Level { Id = 1, LevelName = "1", Description = "Low" },
            new Level { Id = 2, LevelName = "2", Description = "Medium" });

            builder.Entity<User>().HasData(new User { Id = 1, FirstName = "John", LastName = "Dove", EmailId = "JDove@gmail.com" },
            new User { Id = 2, FirstName = "John1", LastName = "Dove1", EmailId = "J1Dove1@gmail.com" });

            builder.Entity<Skill>().HasData(new Skill { Id = 1, SkillName = ".Net core", Description = ".Net" },
            new Skill { Id = 2, SkillName = "Asp.Net core", Description = ".Net" });


            builder.Entity<UserSkillLevel>().HasData(new UserSkillLevel { UserId = 1, SkillId = 1, LevelId = 1 },
            new UserSkillLevel { UserId = 1, SkillId = 2, LevelId = 2 });
        }

        public override int SaveChanges()
        {
            //var changedEntities = ChangeTracker.Entries().Where(changeteacker => changeteacker.State == EntityState
            //.Added || changeteacker.State == EntityState.Modified);

            //foreach (var changedEntity in changedEntities)
            //{
            //    changedEntity.Property("UpdatedDate").CurrentValue = DateTime.Now;

            //    if (changedEntity.State == EntityState.Added)
            //    {
            //        changedEntity.Property("CreatedDate").CurrentValue = DateTime.Now;
            //    }
            //}

            return base.SaveChanges();
        }
    }

}