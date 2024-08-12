﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Movie_Booking.Model;

#nullable disable

namespace Movie_Booking.Migrations
{
    [DbContext(typeof(MovieBookingDbContext))]
    [Migration("20240124184903_seven")]
    partial class seven
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Movie_Booking.Model.Admin", b =>
                {
                    b.Property<int>("adminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("adminId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("emailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("adminId");

                    b.ToTable("admins");
                });

            modelBuilder.Entity("Movie_Booking.Model.Booking", b =>
                {
                    b.Property<int>("seatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("seatId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<int>("Theater_id")
                        .HasColumnType("int");

                    b.Property<bool>("is_available")
                        .HasColumnType("bit");

                    b.Property<int>("seatnumbet")
                        .HasColumnType("int");

                    b.Property<string>("timings")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("seatId");

                    b.HasIndex("Theater_id");

                    b.ToTable("bookings");
                });

            modelBuilder.Entity("Movie_Booking.Model.Movie", b =>
                {
                    b.Property<int>("movieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("movieId"));

                    b.Property<string>("Director")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("Date");

                    b.Property<string>("HeroName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeroineName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Producer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("Date");

                    b.Property<string>("RunTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("movieDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("movieName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("movie_poster")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("musicDirector")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("movieId");

                    b.ToTable("movies");
                });

            modelBuilder.Entity("Movie_Booking.Model.Theaters", b =>
                {
                    b.Property<int>("Theater_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Theater_id"));

                    b.Property<string>("Theater_Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Theater_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("movieid")
                        .HasColumnType("int");

                    b.HasKey("Theater_id");

                    b.HasIndex("movieid");

                    b.ToTable("theaters");
                });

            modelBuilder.Entity("Movie_Booking.Model.TicketBooking", b =>
                {
                    b.Property<int>("ticketsid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ticketsid"));

                    b.Property<DateTime>("date")
                        .HasColumnType("Date");

                    b.Property<string>("mail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("movieName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("seatid")
                        .HasColumnType("int");

                    b.Property<int>("seatnumber")
                        .HasColumnType("int");

                    b.Property<string>("theateraddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("theatrename")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("time")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ticketsid");

                    b.ToTable("ticketBookings");
                });

            modelBuilder.Entity("Movie_Booking.Model.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("userId"));

                    b.Property<string>("emailId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Movie_Booking.Model.Booking", b =>
                {
                    b.HasOne("Movie_Booking.Model.Theaters", "Theaters")
                        .WithMany()
                        .HasForeignKey("Theater_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Theaters");
                });

            modelBuilder.Entity("Movie_Booking.Model.Theaters", b =>
                {
                    b.HasOne("Movie_Booking.Model.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("movieid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });
#pragma warning restore 612, 618
        }
    }
}
