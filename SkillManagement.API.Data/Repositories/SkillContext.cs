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
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public DbSet<Level> Levels { get; set; }

        public DbSet<EmployeeSkillLevel> EmployeeSkillLevels { get; set; }

        

        public DbSet<Roles> Roles { get; set; }
        public DbSet<EmployeeRoles> EmployeeRoles { get; set; }
        public SkillContext(DbContextOptions<SkillContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Employee>().ToTable("Employees");
            builder.Entity<Employee>().HasKey(p => p.Id);
            builder.Entity<Employee>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Employee>().Property(p => p.FirstName).IsRequired().HasMaxLength(100);
            builder.Entity<Employee>().Property(p => p.LastName).HasMaxLength(100);
            builder.Entity<Employee>().Property(p => p.EmailId).HasMaxLength(100);
            

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

            //builder.Entity<User>().ToTable("Users");
            //builder.Entity<User>().HasKey(p => p.Id);
            //builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            //builder.Entity<User>().Property(p => p.UserName).IsRequired().HasMaxLength(100);
            //builder.Entity<User>().Property(p => p.Email).HasMaxLength(100);
            //builder.Entity<User>().Property(p => p.Password).HasMaxLength(100);
            //builder.Entity<User>().HasMany<User_Roles>(sc => sc.User_Roles).WithOne(s => s.User).HasForeignKey(sc => sc.UserId);

            builder.Entity<Roles>().ToTable("Roles");
            builder.Entity<Roles>().HasKey(p => p.Id);
            builder.Entity<Roles>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Roles>().Property(p => p.Role).IsRequired().HasMaxLength(100);
            builder.Entity<Roles>().HasMany<EmployeeRoles>(sc => sc.EmployeeRoles).WithOne(s => s.Roles).HasForeignKey(sc => sc.RoleId);

            builder.Entity<EmployeeRoles>().ToTable("EmployeeRoles").HasKey(sc => new { sc.RoleId, sc.EmployeeId});
            builder.Entity<EmployeeRoles>().HasOne<Employee>(sc => sc.Employee).WithMany(s => s.EmployeeRoles).HasForeignKey(sc => sc.EmployeeId);
            builder.Entity<EmployeeRoles>().HasOne<Roles>(sc => sc.Roles).WithMany(s => s.EmployeeRoles).HasForeignKey(sc => sc.RoleId);


            builder.Entity<EmployeeSkillLevel>().ToTable("EmployeesSkillLevel");
            builder.Entity<EmployeeSkillLevel>().HasKey(p => p.Id);
            builder.Entity<EmployeeSkillLevel>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            //builder.Entity<UserSkillLevel>().HasKey(p => new { p.UserId, p.SkillId, p.LevelId });
            builder.Entity<EmployeeSkillLevel>().HasOne<Employee>(p => p.Employee).WithMany(p => p.EmployeeSkillLevel).HasForeignKey(p => p.EmployeeId);
            builder.Entity<EmployeeSkillLevel>().HasOne<Skill>(p => p.Skill).WithMany(p => p.EmployeeSkillLevel).HasForeignKey(p => p.SkillId);
            builder.Entity<EmployeeSkillLevel>().HasOne<Level>(p => p.Level).WithMany(p => p.EmployeeSkillLevel).HasForeignKey(p => p.LevelId);
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