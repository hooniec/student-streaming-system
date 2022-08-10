﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StreamTec.Models;

#nullable disable

namespace StreamTec.Migrations
{
    [DbContext(typeof(WelTecContext))]
    [Migration("20220809033614_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("StreamTec.Models.Stream", b =>
                {
                    b.Property<string>("StreamID")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("Credits")
                        .HasColumnType("int");

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("EndTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Room")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("StartTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StreamID");

                    b.ToTable("Stream", (string)null);
                });

            modelBuilder.Entity("StreamTec.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.ToTable("Student", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}