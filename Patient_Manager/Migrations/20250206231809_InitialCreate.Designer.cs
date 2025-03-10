﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Patient_Manager.Data;

#nullable disable

namespace Patient_Manager.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250206231809_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "pg_stat_statements");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Patient_Manager.Models.Checkup", b =>
                {
                    b.Property<int>("Checkupid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("checkupid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Checkupid"));

                    b.Property<DateOnly>("Checkupdate")
                        .HasColumnType("date")
                        .HasColumnName("checkupdate");

                    b.Property<TimeOnly>("Checkuptime")
                        .HasColumnType("time without time zone")
                        .HasColumnName("checkuptime");

                    b.Property<string>("Checkuptype")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("checkuptype");

                    b.Property<int>("Patientid")
                        .HasColumnType("integer")
                        .HasColumnName("patientid");

                    b.HasKey("Checkupid")
                        .HasName("checkups_pkey");

                    b.HasIndex("Patientid");

                    b.ToTable("checkups", (string)null);
                });

            modelBuilder.Entity("Patient_Manager.Models.Medicalfile", b =>
                {
                    b.Property<int>("Fileid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("fileid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Fileid"));

                    b.Property<int>("Checkupid")
                        .HasColumnType("integer")
                        .HasColumnName("checkupid");

                    b.Property<string>("Filepath")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("filepath");

                    b.Property<DateTime?>("Uploadedat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("uploadedat")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Fileid")
                        .HasName("medicalfiles_pkey");

                    b.HasIndex("Checkupid");

                    b.ToTable("medicalfiles", (string)null);
                });

            modelBuilder.Entity("Patient_Manager.Models.Medicalrecord", b =>
                {
                    b.Property<int>("Recordid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("recordid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Recordid"));

                    b.Property<string>("Diseasename")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("diseasename");

                    b.Property<DateOnly?>("Enddate")
                        .HasColumnType("date")
                        .HasColumnName("enddate");

                    b.Property<int>("Patientid")
                        .HasColumnType("integer")
                        .HasColumnName("patientid");

                    b.Property<DateOnly>("Startdate")
                        .HasColumnType("date")
                        .HasColumnName("startdate");

                    b.HasKey("Recordid")
                        .HasName("medicalrecords_pkey");

                    b.HasIndex("Patientid");

                    b.ToTable("medicalrecords", (string)null);
                });

            modelBuilder.Entity("Patient_Manager.Models.Patient", b =>
                {
                    b.Property<int>("Patientid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("patientid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Patientid"));

                    b.Property<DateOnly>("Dateofbirth")
                        .HasColumnType("date")
                        .HasColumnName("dateofbirth");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("firstname");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("lastname");

                    b.Property<char?>("Sex")
                        .HasMaxLength(1)
                        .HasColumnType("character(1)")
                        .HasColumnName("sex");

                    b.HasKey("Patientid")
                        .HasName("patients_pkey");

                    b.ToTable("patients", (string)null);
                });

            modelBuilder.Entity("Patient_Manager.Models.Prescription", b =>
                {
                    b.Property<int>("Prescriptionid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("prescriptionid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Prescriptionid"));

                    b.Property<int>("Checkupid")
                        .HasColumnType("integer")
                        .HasColumnName("checkupid");

                    b.Property<string>("Dosage")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("dosage");

                    b.Property<string>("Medication")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("medication");

                    b.HasKey("Prescriptionid")
                        .HasName("prescriptions_pkey");

                    b.HasIndex("Checkupid");

                    b.ToTable("prescriptions", (string)null);
                });

            modelBuilder.Entity("Patient_Manager.Models.Checkup", b =>
                {
                    b.HasOne("Patient_Manager.Models.Patient", "Patient")
                        .WithMany("Checkups")
                        .HasForeignKey("Patientid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("checkups_patientid_fkey");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Patient_Manager.Models.Medicalfile", b =>
                {
                    b.HasOne("Patient_Manager.Models.Checkup", "Checkup")
                        .WithMany("Medicalfiles")
                        .HasForeignKey("Checkupid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("medicalfiles_checkupid_fkey");

                    b.Navigation("Checkup");
                });

            modelBuilder.Entity("Patient_Manager.Models.Medicalrecord", b =>
                {
                    b.HasOne("Patient_Manager.Models.Patient", "Patient")
                        .WithMany("Medicalrecords")
                        .HasForeignKey("Patientid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("medicalrecords_patientid_fkey");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Patient_Manager.Models.Prescription", b =>
                {
                    b.HasOne("Patient_Manager.Models.Checkup", "Checkup")
                        .WithMany("Prescriptions")
                        .HasForeignKey("Checkupid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("prescriptions_checkupid_fkey");

                    b.Navigation("Checkup");
                });

            modelBuilder.Entity("Patient_Manager.Models.Checkup", b =>
                {
                    b.Navigation("Medicalfiles");

                    b.Navigation("Prescriptions");
                });

            modelBuilder.Entity("Patient_Manager.Models.Patient", b =>
                {
                    b.Navigation("Checkups");

                    b.Navigation("Medicalrecords");
                });
#pragma warning restore 612, 618
        }
    }
}
