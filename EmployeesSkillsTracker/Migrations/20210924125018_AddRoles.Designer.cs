﻿// <auto-generated />
using System;
using EmployeesSkillsTracker.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeesSkillsTracker.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210924125018_AddRoles")]
    partial class AddRoles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EmployeesSkillsTracker.Entities.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("YearsOfExperience")
                        .HasColumnType("float");

                    b.HasKey("EmployeeID");

                    b.HasIndex("RoleId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeID = 1,
                            Email = "a@e.com",
                            FirstName = "a",
                            LastName = "A",
                            Password = "123456",
                            PhoneNumber = "123",
                            Position = "Developer",
                            RoleId = 2,
                            Salary = 1000,
                            Username = "a",
                            YearsOfExperience = 1.0
                        },
                        new
                        {
                            EmployeeID = 2,
                            Email = "b@e.com",
                            FirstName = "b",
                            LastName = "A",
                            Password = "123456",
                            PhoneNumber = "456",
                            Position = "Developer",
                            RoleId = 2,
                            Salary = 1000,
                            Username = "b",
                            YearsOfExperience = 2.0
                        },
                        new
                        {
                            EmployeeID = 3,
                            Email = "c@e.com",
                            FirstName = "c",
                            LastName = "A",
                            Password = "123456",
                            PhoneNumber = "789",
                            Position = "Project Manager",
                            RoleId = 2,
                            Salary = 2000,
                            Username = "c",
                            YearsOfExperience = 2.0
                        },
                        new
                        {
                            EmployeeID = 4,
                            Email = "d@e.com",
                            FirstName = "d",
                            LastName = "A",
                            Password = "123456",
                            PhoneNumber = "012",
                            Position = "Manager",
                            RoleId = 2,
                            Salary = 3000,
                            Username = "d",
                            YearsOfExperience = 3.0
                        },
                        new
                        {
                            EmployeeID = 5,
                            Email = "e@e.com",
                            FirstName = "e",
                            LastName = "A",
                            Password = "123456",
                            PhoneNumber = "234",
                            Position = "Admin",
                            RoleId = 1,
                            Salary = 4000,
                            Username = "e",
                            YearsOfExperience = 2.0
                        });
                });

            modelBuilder.Entity("EmployeesSkillsTracker.Entities.EmployeeSkill", b =>
                {
                    b.Property<int>("EmployeeSkillID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SkillID")
                        .HasColumnType("int");

                    b.HasKey("EmployeeSkillID");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("SkillID");

                    b.ToTable("EmployeesSkills");

                    b.HasData(
                        new
                        {
                            EmployeeSkillID = 1,
                            EmployeeID = 1,
                            Level = "Beginner",
                            SkillID = 1
                        },
                        new
                        {
                            EmployeeSkillID = 2,
                            EmployeeID = 1,
                            Level = "Intermediate",
                            SkillID = 2
                        },
                        new
                        {
                            EmployeeSkillID = 3,
                            EmployeeID = 1,
                            Level = "Advanced",
                            SkillID = 3
                        },
                        new
                        {
                            EmployeeSkillID = 4,
                            EmployeeID = 2,
                            Level = "Beginner",
                            SkillID = 4
                        },
                        new
                        {
                            EmployeeSkillID = 5,
                            EmployeeID = 2,
                            Level = "Beginner",
                            SkillID = 5
                        },
                        new
                        {
                            EmployeeSkillID = 6,
                            EmployeeID = 3,
                            Level = "Advanced",
                            SkillID = 6
                        },
                        new
                        {
                            EmployeeSkillID = 7,
                            EmployeeID = 3,
                            Level = "Beginner",
                            SkillID = 7
                        },
                        new
                        {
                            EmployeeSkillID = 8,
                            EmployeeID = 4,
                            Level = "Beginner",
                            SkillID = 8
                        },
                        new
                        {
                            EmployeeSkillID = 9,
                            EmployeeID = 4,
                            Level = "Intermediate",
                            SkillID = 1
                        },
                        new
                        {
                            EmployeeSkillID = 10,
                            EmployeeID = 4,
                            Level = "Intermediate",
                            SkillID = 3
                        },
                        new
                        {
                            EmployeeSkillID = 11,
                            EmployeeID = 5,
                            Level = "Intermediate",
                            SkillID = 4
                        },
                        new
                        {
                            EmployeeSkillID = 12,
                            EmployeeID = 5,
                            Level = "Intermediate",
                            SkillID = 7
                        },
                        new
                        {
                            EmployeeSkillID = 13,
                            EmployeeID = 5,
                            Level = "Intermediate",
                            SkillID = 8
                        },
                        new
                        {
                            EmployeeSkillID = 14,
                            EmployeeID = 2,
                            Level = "Intermediate",
                            SkillID = 7
                        },
                        new
                        {
                            EmployeeSkillID = 15,
                            EmployeeID = 2,
                            Level = "Beginner",
                            SkillID = 8
                        },
                        new
                        {
                            EmployeeSkillID = 16,
                            EmployeeID = 2,
                            Level = "Advanced",
                            SkillID = 1
                        },
                        new
                        {
                            EmployeeSkillID = 17,
                            EmployeeID = 1,
                            Level = "Beginner",
                            SkillID = 6
                        });
                });

            modelBuilder.Entity("EmployeesSkillsTracker.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            RoleId = 2,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("EmployeesSkillsTracker.Entities.Skill", b =>
                {
                    b.Property<int>("SkillID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Vertical")
                        .HasColumnType("int");

                    b.HasKey("SkillID");

                    b.ToTable("Skills");

                    b.HasData(
                        new
                        {
                            SkillID = 1,
                            Name = "Blue Prism",
                            Vertical = 0
                        },
                        new
                        {
                            SkillID = 2,
                            Name = "UiPath",
                            Vertical = 0
                        },
                        new
                        {
                            SkillID = 3,
                            Name = "Knime",
                            Vertical = 0
                        },
                        new
                        {
                            SkillID = 4,
                            Name = "Java",
                            Vertical = 0
                        },
                        new
                        {
                            SkillID = 5,
                            Name = "Python",
                            Vertical = 0
                        },
                        new
                        {
                            SkillID = 6,
                            Name = "PowerBI",
                            Vertical = 0
                        },
                        new
                        {
                            SkillID = 7,
                            Name = "PowerApps",
                            Vertical = 0
                        },
                        new
                        {
                            SkillID = 8,
                            Name = "VBA",
                            Vertical = 0
                        });
                });

            modelBuilder.Entity("EmployeesSkillsTracker.Entities.Employee", b =>
                {
                    b.HasOne("EmployeesSkillsTracker.Entities.Role", "Role")
                        .WithMany("Employees")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("EmployeesSkillsTracker.Entities.EmployeeSkill", b =>
                {
                    b.HasOne("EmployeesSkillsTracker.Entities.Employee", "Employee")
                        .WithMany("Skills")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeesSkillsTracker.Entities.Skill", "Skill")
                        .WithMany("Employees")
                        .HasForeignKey("SkillID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("EmployeesSkillsTracker.Entities.Employee", b =>
                {
                    b.Navigation("Skills");
                });

            modelBuilder.Entity("EmployeesSkillsTracker.Entities.Role", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("EmployeesSkillsTracker.Entities.Skill", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
