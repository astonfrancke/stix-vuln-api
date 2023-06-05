﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StixVuln.Infrastructure.Data;

#nullable disable

namespace StixVuln.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StixVuln.Core.Authentication.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("IdentityId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("StixVuln.Core.Identity.Identity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ContactInformation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByRef")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Identitites", (string)null);
                });

            modelBuilder.Entity("StixVuln.Core.Vulnerability.Vulnerability", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByRef")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vulnerabilities", (string)null);
                });

            modelBuilder.Entity("StixVuln.Core.Identity.Identity", b =>
                {
                    b.OwnsOne("StixVuln.Core.Identity.IdentityClass", "IdentityClass", b1 =>
                        {
                            b1.Property<int>("IdentityClassId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("IdentityClassId"));

                            b1.Property<string>("Id")
                                .IsRequired()
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("IdentityClassId");

                            b1.HasIndex("Id")
                                .IsUnique();

                            b1.ToTable("IdentityClasses", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("Id");
                        });

                    b.OwnsMany("StixVuln.Core.Identity.Role", "Roles", b1 =>
                        {
                            b1.Property<int>("RoleId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("RoleId"));

                            b1.Property<string>("Id")
                                .IsRequired()
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("RoleId");

                            b1.HasIndex("Id");

                            b1.ToTable("Roles", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("Id");
                        });

                    b.OwnsMany("StixVuln.Core.Identity.Sector", "Sectors", b1 =>
                        {
                            b1.Property<int>("SectorId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("SectorId"));

                            b1.Property<string>("Id")
                                .IsRequired()
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("SectorId");

                            b1.HasIndex("Id");

                            b1.ToTable("Sectors", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("Id");
                        });

                    b.Navigation("IdentityClass")
                        .IsRequired();

                    b.Navigation("Roles");

                    b.Navigation("Sectors");
                });

            modelBuilder.Entity("StixVuln.Core.Vulnerability.Vulnerability", b =>
                {
                    b.OwnsMany("StixVuln.Core.ExternalReference", "ExternalReferences", b1 =>
                        {
                            b1.Property<int>("ExternalReferenceId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("ExternalReferenceId"));

                            b1.Property<string>("Description")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ExternalId")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Id")
                                .IsRequired()
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("SourceName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Url")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ExternalReferenceId");

                            b1.HasIndex("Id");

                            b1.ToTable("ExternalReferences", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("Id");
                        });

                    b.Navigation("ExternalReferences");
                });
#pragma warning restore 612, 618
        }
    }
}
