﻿// <auto-generated />
using System;
using AATAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AATAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230911103935_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AATAPI.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TotalSeats")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AvailableSeats = 100,
                            Date = new DateTime(2023, 9, 11, 12, 39, 35, 902, DateTimeKind.Local).AddTicks(5902),
                            Name = "Event 1",
                            TotalSeats = 100
                        },
                        new
                        {
                            Id = 2,
                            AvailableSeats = 50,
                            Date = new DateTime(2023, 9, 18, 12, 39, 35, 902, DateTimeKind.Local).AddTicks(5911),
                            Name = "Event 2",
                            TotalSeats = 50
                        });
                });

            modelBuilder.Entity("AATAPI.Entities.Registration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReferenceNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserIdentifier")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("ReferenceNumber")
                        .IsUnique();

                    b.ToTable("Registrations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "user1@example.com",
                            EventId = 1,
                            Name = "User One",
                            ReferenceNumber = "ABC123",
                            RegistrationDate = new DateTime(2023, 9, 11, 12, 39, 35, 902, DateTimeKind.Local).AddTicks(5997),
                            UserId = new Guid("d74e5dd7-a10e-418e-81fc-9265299f2424"),
                            UserIdentifier = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = 2,
                            Email = "user2@example.com",
                            EventId = 1,
                            Name = "User Two",
                            ReferenceNumber = "XYZ456",
                            RegistrationDate = new DateTime(2023, 9, 11, 12, 39, 35, 902, DateTimeKind.Local).AddTicks(5998),
                            UserId = new Guid("e5ceb6a0-69eb-4de2-a29a-413533968096"),
                            UserIdentifier = new Guid("00000000-0000-0000-0000-000000000000")
                        });
                });

            modelBuilder.Entity("AATAPI.Entities.Registration", b =>
                {
                    b.HasOne("AATAPI.Entities.Event", "Event")
                        .WithMany("Registrations")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("AATAPI.Entities.Event", b =>
                {
                    b.Navigation("Registrations");
                });
#pragma warning restore 612, 618
        }
    }
}
