﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Service.Audience.Context;

#nullable disable

namespace Service.Audience.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Service.Audience.Models.EF.EFAudField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("EFAudCustomFields");
                });

            modelBuilder.Entity("Service.Audience.Models.EF.EFAudValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AudienceId")
                        .HasColumnType("integer");

                    b.Property<int>("CustomFieldId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AudienceId");

                    b.HasIndex("CustomFieldId");

                    b.ToTable("EFAudCustomFieldsValues");
                });

            modelBuilder.Entity("Service.Audience.Models.EF.EFSoftware", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AudienceId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LicenseExpirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LicenseKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("LicenseType")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NumberPC")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AudienceId");

                    b.ToTable("EFSoftware");
                });

            modelBuilder.Entity("Service.Audience.Models.EFModels.EFAudience", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AudienceType")
                        .HasColumnType("integer");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<int?>("Floor")
                        .HasColumnType("integer");

                    b.Property<int?>("HousingId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HousingId");

                    b.ToTable("EFAudiences");
                });

            modelBuilder.Entity("Service.Audience.Models.EFModels.EFHousingSummary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EFHousingSummary");
                });

            modelBuilder.Entity("Service.Audience.Models.EF.EFAudValue", b =>
                {
                    b.HasOne("Service.Audience.Models.EFModels.EFAudience", "Audience")
                        .WithMany("CustomFieldValues")
                        .HasForeignKey("AudienceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Service.Audience.Models.EF.EFAudField", "CustomField")
                        .WithMany()
                        .HasForeignKey("CustomFieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Audience");

                    b.Navigation("CustomField");
                });

            modelBuilder.Entity("Service.Audience.Models.EF.EFSoftware", b =>
                {
                    b.HasOne("Service.Audience.Models.EFModels.EFAudience", "Audience")
                        .WithMany()
                        .HasForeignKey("AudienceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Audience");
                });

            modelBuilder.Entity("Service.Audience.Models.EFModels.EFAudience", b =>
                {
                    b.HasOne("Service.Audience.Models.EFModels.EFHousingSummary", "Housing")
                        .WithMany()
                        .HasForeignKey("HousingId");

                    b.Navigation("Housing");
                });

            modelBuilder.Entity("Service.Audience.Models.EFModels.EFAudience", b =>
                {
                    b.Navigation("CustomFieldValues");
                });
#pragma warning restore 612, 618
        }
    }
}
