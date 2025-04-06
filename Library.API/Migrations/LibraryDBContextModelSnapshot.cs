﻿// <auto-generated />
using System;
using Library.API.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Library.API.Migrations
{
    [DbContext(typeof(LibraryDBContext))]
    partial class LibraryDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.3");

            modelBuilder.Entity("Library.API.Database.Entities.LibraryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("author");

                    b.Property<string>("BookName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("book_name");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_date");

                    b.Property<DateTime>("PublicationDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("publication_date");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("publisher");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("type");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_date");

                    b.Property<int>("Volume")
                        .HasColumnType("INTEGER")
                        .HasColumnName("volume");

                    b.HasKey("Id");

                    b.ToTable("Library");
                });
#pragma warning restore 612, 618
        }
    }
}
