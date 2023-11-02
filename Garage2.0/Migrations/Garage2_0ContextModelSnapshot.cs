﻿// <auto-generated />
using System;
using Garage2._0.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Garage2._0.Migrations
{
    [DbContext(typeof(Garage2_0Context))]
    partial class Garage2_0ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Garage2._0.Models.Entities.ParkeratFordon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AnkomstTid")
                        .HasColumnType("datetime2");

                    b.Property<int?>("AntalHjul")
                        .HasColumnType("int");

                    b.Property<string>("Farg")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("FordonsTyp")
                        .HasColumnType("int");

                    b.Property<string>("Marke")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Modell")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("RegNr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ParkeratFordon");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AnkomstTid = new DateTime(2023, 11, 2, 12, 15, 0, 0, DateTimeKind.Unspecified),
                            AntalHjul = 4,
                            Farg = "Röd",
                            FordonsTyp = 1,
                            Marke = "Toyota",
                            Modell = "Prius",
                            RegNr = "123pop"
                        },
                        new
                        {
                            Id = 2,
                            AnkomstTid = new DateTime(2023, 11, 2, 12, 15, 0, 0, DateTimeKind.Unspecified),
                            AntalHjul = 0,
                            Farg = "Vit",
                            FordonsTyp = 2,
                            Marke = "Storebror",
                            Modell = "Japp",
                            RegNr = "123båt"
                        },
                        new
                        {
                            Id = 3,
                            AnkomstTid = new DateTime(2023, 11, 2, 12, 45, 0, 0, DateTimeKind.Unspecified),
                            AntalHjul = 6,
                            Farg = "Blå",
                            FordonsTyp = 4,
                            Marke = "Volvo",
                            Modell = "V70",
                            RegNr = "456pop"
                        },
                        new
                        {
                            Id = 4,
                            AnkomstTid = new DateTime(2023, 11, 2, 12, 25, 0, 0, DateTimeKind.Unspecified),
                            AntalHjul = 8,
                            Farg = "Vit",
                            FordonsTyp = 0,
                            Marke = "Airbus",
                            Modell = "XX90",
                            RegNr = "783pop"
                        },
                        new
                        {
                            Id = 5,
                            AnkomstTid = new DateTime(2023, 11, 2, 12, 55, 0, 0, DateTimeKind.Unspecified),
                            AntalHjul = 2,
                            Farg = "Svart",
                            FordonsTyp = 3,
                            Marke = "Mazda",
                            Modell = "Vroom",
                            RegNr = "098pop"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
