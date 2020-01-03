﻿// <auto-generated />
using System;
using CarMaintenance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarMaintenance.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200103032550_InitialCarMaintenanceMigration")]
    partial class InitialCarMaintenanceMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CarMaintenance.Models.Car", b =>
                {
                    b.Property<string>("Vin")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("YearManufactured")
                        .HasColumnType("int");

                    b.HasKey("Vin");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("CarMaintenance.Models.OilChange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateTime")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("Mileage")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Vin")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("Vin");

                    b.ToTable("OilChanges");
                });

            modelBuilder.Entity("CarMaintenance.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("CarVin")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CarVin");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CarMaintenance.Models.OilChange", b =>
                {
                    b.HasOne("CarMaintenance.Models.Car", null)
                        .WithMany("OilChanges")
                        .HasForeignKey("Vin")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarMaintenance.Models.User", b =>
                {
                    b.HasOne("CarMaintenance.Models.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarVin");
                });
#pragma warning restore 612, 618
        }
    }
}
