﻿// <auto-generated />
using System;
using MagicVilla_webapi.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicVilla_webapi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230303074956_AddForeignKeyToVillaTable")]
    partial class AddForeignKeyToVillaTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicVilla_webapi.Models.Villa", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("occupancy")
                        .HasColumnType("int");

                    b.Property<double>("rate")
                        .HasColumnType("float");

                    b.Property<int>("sqft")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Villa");

                    b.HasData(
                        new
                        {
                            id = 1,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                            name = "Royal Villa",
                            occupancy = 4,
                            rate = 200.0,
                            sqft = 550
                        },
                        new
                        {
                            id = 2,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                            name = "Premium Pool Villa",
                            occupancy = 0,
                            rate = 300.0,
                            sqft = 550
                        },
                        new
                        {
                            id = 3,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                            name = "Luxury Pool Villa",
                            occupancy = 4,
                            rate = 400.0,
                            sqft = 750
                        },
                        new
                        {
                            id = 4,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                            name = "Diamond Villa",
                            occupancy = 4,
                            rate = 550.0,
                            sqft = 900
                        },
                        new
                        {
                            id = 5,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                            name = "Diamond Pool Villa",
                            occupancy = 4,
                            rate = 600.0,
                            sqft = 1100
                        });
                });

            modelBuilder.Entity("MagicVilla_webapi.Models.VillaNumber", b =>
                {
                    b.Property<int>("VillaNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SpecialDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("VillaID")
                        .HasColumnType("int");

                    b.HasKey("VillaNo");

                    b.HasIndex("VillaID");

                    b.ToTable("VillaNumbers");
                });

            modelBuilder.Entity("MagicVilla_webapi.Models.VillaNumber", b =>
                {
                    b.HasOne("MagicVilla_webapi.Models.Villa", "Villa")
                        .WithMany()
                        .HasForeignKey("VillaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Villa");
                });
#pragma warning restore 612, 618
        }
    }
}
