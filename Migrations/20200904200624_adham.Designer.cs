﻿// <auto-generated />
using System;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ecommerce.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200904200624_adham")]
    partial class adham
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Ecommerce.Models.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActionBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("ActionOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<double>("OfferAmount")
                        .HasColumnType("float");

                    b.Property<int>("OfferPersent")
                        .HasColumnType("int");

                    b.Property<int>("OfferStatus")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Ecommerce.Models.Item", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActionBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("ActionOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<string>("ItemDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<double>("SellPrice")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.ToTable("Items");
                });
#pragma warning restore 612, 618
        }
    }
}