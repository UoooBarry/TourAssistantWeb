﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TourWebApp.Data;

namespace TourWebApp.Migrations
{
    [DbContext(typeof(TourContext))]
    [Migration("20200411125601_newLocationSet")]
    partial class newLocationSet
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TourWebApp.Models.Location", b =>
                {
                    b.Property<int>("LocationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<int?>("LocationSetID")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("MinTime")
                        .HasColumnType("time");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<float>("X")
                        .HasColumnType("real");

                    b.Property<float>("Y")
                        .HasColumnType("real");

                    b.HasKey("LocationID");

                    b.HasIndex("LocationSetID");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("TourWebApp.Models.LocationSet", b =>
                {
                    b.Property<int>("LocationSetID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LocationSetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocationSetID");

                    b.ToTable("LocationSets");
                });

            modelBuilder.Entity("TourWebApp.Models.Login", b =>
                {
                    b.Property<int>("LoginID")
                        .HasColumnType("int")
                        .HasMaxLength(8);

                    b.Property<bool>("ActivationStatus")
                        .HasColumnType("bit");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("LoginID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Logins");

                    b.HasCheckConstraint("CH_Login_LoginID", "len(LoginID) = 8");

                    b.HasCheckConstraint("CH_Login_PasswordHash", "len(PasswordHash) = 64");
                });

            modelBuilder.Entity("TourWebApp.Models.Tour", b =>
                {
                    b.Property<int>("TourID")
                        .HasColumnType("int");

                    b.Property<int>("LocationSetID")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("MinDuration")
                        .HasColumnType("time");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("TourTypeID")
                        .HasColumnType("int");

                    b.HasKey("TourID");

                    b.HasIndex("LocationSetID");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("TourWebApp.Models.TourType", b =>
                {
                    b.Property<int>("TourTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("TourTypeID");

                    b.ToTable("TourTypes");
                });

            modelBuilder.Entity("TourWebApp.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("LoginID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TourWebApp.Models.Location", b =>
                {
                    b.HasOne("TourWebApp.Models.LocationSet", null)
                        .WithMany("Locations")
                        .HasForeignKey("LocationSetID");
                });

            modelBuilder.Entity("TourWebApp.Models.Login", b =>
                {
                    b.HasOne("TourWebApp.Models.User", "user")
                        .WithOne("Login")
                        .HasForeignKey("TourWebApp.Models.Login", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TourWebApp.Models.Tour", b =>
                {
                    b.HasOne("TourWebApp.Models.LocationSet", "LocationSets")
                        .WithMany("Tour")
                        .HasForeignKey("LocationSetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TourWebApp.Models.TourType", "Type")
                        .WithMany("Tour")
                        .HasForeignKey("TourID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
