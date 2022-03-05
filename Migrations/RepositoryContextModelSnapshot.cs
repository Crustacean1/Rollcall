﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rollcall.Repositories;

#nullable disable

namespace rollcall.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Rollcall.Models.Attendance", b =>
                {
                    b.Property<int>("ChildId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<uint>("Meals")
                        .HasColumnType("int unsigned");

                    b.HasKey("ChildId", "Date");

                    b.ToTable("Attendance");
                });

            modelBuilder.Entity("Rollcall.Models.Child", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<uint>("DefaultMeals")
                        .HasColumnType("int unsigned");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Surname")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Children");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DefaultMeals = 3u,
                            GroupId = 1,
                            Name = "Kamil",
                            Surname = "Kowalski"
                        });
                });

            modelBuilder.Entity("Rollcall.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "AEII"
                        });
                });

            modelBuilder.Entity("Rollcall.Models.Mask", b =>
                {
                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<uint>("Meals")
                        .HasColumnType("int unsigned");

                    b.HasKey("GroupId", "Date");

                    b.ToTable("Masks");
                });

            modelBuilder.Entity("Rollcall.Models.MealSchema", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<uint>("Mask")
                        .HasColumnType("int unsigned");

                    b.HasKey("Name");

                    b.ToTable("MealSchemas");

                    b.HasData(
                        new
                        {
                            Name = "breakfast",
                            Mask = 1u
                        },
                        new
                        {
                            Name = "dinner",
                            Mask = 2u
                        },
                        new
                        {
                            Name = "desert",
                            Mask = 4u
                        });
                });

            modelBuilder.Entity("Rollcall.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Rollcall.Models.Attendance", b =>
                {
                    b.HasOne("Rollcall.Models.Child", "TargetChild")
                        .WithMany("MyAttendance")
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TargetChild");
                });

            modelBuilder.Entity("Rollcall.Models.Child", b =>
                {
                    b.HasOne("Rollcall.Models.Group", "MyGroup")
                        .WithMany("Children")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MyGroup");
                });

            modelBuilder.Entity("Rollcall.Models.Mask", b =>
                {
                    b.HasOne("Rollcall.Models.Group", "TargetGroup")
                        .WithMany("Masks")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TargetGroup");
                });

            modelBuilder.Entity("Rollcall.Models.Child", b =>
                {
                    b.Navigation("MyAttendance");
                });

            modelBuilder.Entity("Rollcall.Models.Group", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("Masks");
                });
#pragma warning restore 612, 618
        }
    }
}
