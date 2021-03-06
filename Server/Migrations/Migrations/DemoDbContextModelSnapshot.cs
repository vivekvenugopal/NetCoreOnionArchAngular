﻿// <auto-generated />
using System;
using Demo.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Migrations.Migrations
{
    [DbContext(typeof(DemoDbContext))]
    partial class DemoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("Demo.Model.Employee", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime?>("DOB");

                    b.Property<DateTime>("DateOfJoining");

                    b.Property<string>("Email");

                    b.Property<string>("EmployeeId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<long?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("Demo.Model.Skill", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long?>("EmployeeId");

                    b.Property<DateTime?>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Technology");

                    b.Property<long?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Skill");
                });

            modelBuilder.Entity("Demo.Model.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActiveUser");

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<int>("InternalUser");

                    b.Property<string>("LastName");

                    b.Property<int>("SuperAdmin");

                    b.Property<long?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ActiveUser = 1,
                            CreatedBy = 1L,
                            CreatedDate = new DateTime(2020, 8, 2, 12, 51, 49, 358, DateTimeKind.Local).AddTicks(9940),
                            Email = "",
                            FirstName = "SuperAdmin",
                            InternalUser = 0,
                            LastName = "SuperAdmin",
                            SuperAdmin = 1,
                            UserName = "SuperAdmin"
                        });
                });

            modelBuilder.Entity("Demo.Model.UserPassword", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("HashSalt");

                    b.Property<string>("Password");

                    b.Property<long?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserPassword");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedBy = 1L,
                            CreatedDate = new DateTime(2020, 8, 2, 12, 51, 49, 361, DateTimeKind.Local).AddTicks(777),
                            HashSalt = "b773faade04c9d5ef57ba05c67d728ec",
                            Password = "2fb98c46650e0684addfdd1684376af49cf7103836397c0cc688075bd7dc4b74",
                            UserId = 1L
                        });
                });

            modelBuilder.Entity("Demo.Model.UserRegistration", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Activated");

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<int>("InternalUser");

                    b.Property<string>("LastName");

                    b.Property<long?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("UserRegistration");
                });

            modelBuilder.Entity("Demo.Model.Skill", b =>
                {
                    b.HasOne("Demo.Model.Employee", "Employee")
                        .WithMany("Skills")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Demo.Model.UserPassword", b =>
                {
                    b.HasOne("Demo.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
