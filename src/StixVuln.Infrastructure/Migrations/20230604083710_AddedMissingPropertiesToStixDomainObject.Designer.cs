﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StixVuln.Infrastructure.Data;

#nullable disable

namespace StixVuln.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230604083710_AddedMissingPropertiesToStixDomainObject")]
    partial class AddedMissingPropertiesToStixDomainObject
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

            modelBuilder.Entity("StixVuln.Core.Vulnerability.Vulnerability", b =>
                {
                    b.OwnsMany("StixVuln.Core.Vulnerability.ExternalReference", "ExternalReferences", b1 =>
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
