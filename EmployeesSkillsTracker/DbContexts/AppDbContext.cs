using EmployeesSkillsTracker.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<EmployeeSkill> EmployeesSkills { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasData(new Employee { EmployeeID = 1, FirstName = "a", Username = "a",  LastName ="A", Password= "AQAAAAEAACcQAAAAEEIOHFMUsnlWhRiA3W8wsiPnnFPvb1FbXU6qw+Az5Nme/q9kw5BfypEm4yHT2Rl6ew==", Email = "a@e.com", PhoneNumber = "123", Salary = 1000, Position = "Developer", YearsOfExperience = 1 , RoleId = 1 });
            modelBuilder.Entity<Employee>().HasData(new Employee { EmployeeID = 2, FirstName = "b", Username = "b", LastName ="A", Password= "AQAAAAEAACcQAAAAEEIOHFMUsnlWhRiA3W8wsiPnnFPvb1FbXU6qw+Az5Nme/q9kw5BfypEm4yHT2Rl6ew==",  Email = "b@e.com", PhoneNumber = "456", Salary = 1000, Position = "Developer", YearsOfExperience = 2 , RoleId = 2 });
            modelBuilder.Entity<Employee>().HasData(new Employee { EmployeeID = 3, FirstName = "c", Username = "c", LastName ="A", Password= "AQAAAAEAACcQAAAAEEIOHFMUsnlWhRiA3W8wsiPnnFPvb1FbXU6qw+Az5Nme/q9kw5BfypEm4yHT2Rl6ew==",  Email = "c@e.com", PhoneNumber = "789", Salary = 2000, Position = "Project Manager", YearsOfExperience = 2, RoleId = 2 });
            modelBuilder.Entity<Employee>().HasData(new Employee { EmployeeID = 4, FirstName = "d", Username = "d", LastName ="A", Password= "AQAAAAEAACcQAAAAEEIOHFMUsnlWhRiA3W8wsiPnnFPvb1FbXU6qw+Az5Nme/q9kw5BfypEm4yHT2Rl6ew==",  Email = "d@e.com", PhoneNumber = "012", Salary = 3000, Position = "Manager", YearsOfExperience = 3, RoleId = 2 });
            modelBuilder.Entity<Employee>().HasData(new Employee { EmployeeID = 5, FirstName = "e", Username = "e", LastName ="A", Password= "AQAAAAEAACcQAAAAEEIOHFMUsnlWhRiA3W8wsiPnnFPvb1FbXU6qw+Az5Nme/q9kw5BfypEm4yHT2Rl6ew==",  Email = "e@e.com", PhoneNumber = "234", Salary = 4000, Position = "Admin", YearsOfExperience = 2, RoleId = 1 });

            modelBuilder.Entity<Skill>().HasData(new Skill { SkillID = 1, Name = "Blue Prism" });
            modelBuilder.Entity<Skill>().HasData(new Skill { SkillID = 2, Name = "UiPath" });
            modelBuilder.Entity<Skill>().HasData(new Skill { SkillID = 3, Name = "Knime" });
            modelBuilder.Entity<Skill>().HasData(new Skill { SkillID = 4, Name = "Java" });
            modelBuilder.Entity<Skill>().HasData(new Skill { SkillID = 5, Name = "Python" });
            modelBuilder.Entity<Skill>().HasData(new Skill { SkillID = 6, Name = "PowerBI" });
            modelBuilder.Entity<Skill>().HasData(new Skill { SkillID = 7, Name = "PowerApps" });
            modelBuilder.Entity<Skill>().HasData(new Skill { SkillID = 8, Name = "VBA" });


            modelBuilder.Entity<Role>().HasData(new Role {RoleId = 1,  Name = "Admin" });
            modelBuilder.Entity<Role>().HasData(new Role {RoleId = 2, Name = "User" });


            modelBuilder.Entity<EmployeeSkill>().HasData(new EmployeeSkill { EmployeeSkillID = 1, EmployeeID = 1, SkillID = 1, Level = "Beginner" });
            modelBuilder.Entity<EmployeeSkill>().HasData(new EmployeeSkill { EmployeeSkillID = 2, EmployeeID = 1, SkillID = 2, Level = "Intermediate" });
            modelBuilder.Entity<EmployeeSkill>().HasData(new EmployeeSkill { EmployeeSkillID = 3, EmployeeID = 1, SkillID = 3, Level = "Advanced" });
            modelBuilder.Entity<EmployeeSkill>().HasData(new EmployeeSkill { EmployeeSkillID = 4, EmployeeID = 2, SkillID = 4, Level = "Beginner" });
            modelBuilder.Entity<EmployeeSkill>().HasData(new EmployeeSkill { EmployeeSkillID = 5, EmployeeID = 2, SkillID = 5, Level = "Beginner" });
            modelBuilder.Entity<EmployeeSkill>().HasData(new EmployeeSkill { EmployeeSkillID = 6, EmployeeID = 3, SkillID = 6, Level = "Advanced" });
            modelBuilder.Entity<EmployeeSkill>().HasData(new EmployeeSkill { EmployeeSkillID = 7, EmployeeID = 3, SkillID = 7, Level = "Beginner" });
            modelBuilder.Entity<EmployeeSkill>().HasData(new EmployeeSkill { EmployeeSkillID = 8, EmployeeID = 4, SkillID = 8, Level = "Beginner" });
            modelBuilder.Entity<EmployeeSkill>().HasData(new EmployeeSkill { EmployeeSkillID = 9, EmployeeID = 4, SkillID = 1, Level = "Intermediate" });
            modelBuilder.Entity<EmployeeSkill>().HasData(new EmployeeSkill { EmployeeSkillID = 10, EmployeeID = 4, SkillID = 3, Level = "Intermediate" });
            modelBuilder.Entity<EmployeeSkill>().HasData(new EmployeeSkill { EmployeeSkillID = 11, EmployeeID = 5, SkillID = 4, Level = "Intermediate" });
            modelBuilder.Entity<EmployeeSkill>().HasData(new EmployeeSkill { EmployeeSkillID = 12, EmployeeID = 5, SkillID = 7, Level = "Intermediate" });
            modelBuilder.Entity<EmployeeSkill>().HasData(new EmployeeSkill { EmployeeSkillID = 13, EmployeeID = 5, SkillID = 8, Level = "Intermediate" });
            modelBuilder.Entity<EmployeeSkill>().HasData(new EmployeeSkill { EmployeeSkillID = 14, EmployeeID = 2, SkillID = 7, Level = "Intermediate" });
            modelBuilder.Entity<EmployeeSkill>().HasData(new EmployeeSkill { EmployeeSkillID = 15, EmployeeID = 2, SkillID = 8, Level = "Beginner" });
            modelBuilder.Entity<EmployeeSkill>().HasData(new EmployeeSkill { EmployeeSkillID = 16, EmployeeID = 2, SkillID = 1, Level = "Advanced" });
            modelBuilder.Entity<EmployeeSkill>().HasData(new EmployeeSkill { EmployeeSkillID = 17, EmployeeID = 1, SkillID = 6, Level = "Beginner" });
        }
    }
}
