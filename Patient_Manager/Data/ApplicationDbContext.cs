using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Patient_Manager.Models;

namespace Patient_Manager.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Checkup> Checkups { get; set; }

    public virtual DbSet<Medicalfile> Medicalfiles { get; set; }

    public virtual DbSet<Medicalrecord> Medicalrecords { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=uncannily-bewitching-schnauzer.data-1.use1.tembo.io;Port=5432;Database=medical_system;Username=postgres;Password=X1XRvlFs2qDXe3IV;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_stat_statements");

        modelBuilder.Entity<Checkup>(entity =>
        {
            entity.HasKey(e => e.Checkupid).HasName("checkups_pkey");

            entity.ToTable("checkups");

            entity.Property(e => e.Checkupid).HasColumnName("checkupid");
            entity.Property(e => e.Checkupdate).HasColumnName("checkupdate");
            entity.Property(e => e.Checkuptime).HasColumnName("checkuptime");
            entity.Property(e => e.Checkuptype)
                .HasMaxLength(20)
                .HasColumnName("checkuptype");
            entity.Property(e => e.Patientid).HasColumnName("patientid");

            entity.HasOne(d => d.Patient).WithMany(p => p.Checkups)
                .HasForeignKey(d => d.Patientid)
                .HasConstraintName("checkups_patientid_fkey");
        });

        modelBuilder.Entity<Medicalfile>(entity =>
        {
            entity.HasKey(e => e.Fileid).HasName("medicalfiles_pkey");

            entity.ToTable("medicalfiles");

            entity.Property(e => e.Fileid).HasColumnName("fileid");
            entity.Property(e => e.Checkupid).HasColumnName("checkupid");
            entity.Property(e => e.Filepath).HasColumnName("filepath");
            entity.Property(e => e.Uploadedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("uploadedat");

            entity.HasOne(d => d.Checkup).WithMany(p => p.Medicalfiles)
                .HasForeignKey(d => d.Checkupid)
                .HasConstraintName("medicalfiles_checkupid_fkey");
        });

        modelBuilder.Entity<Medicalrecord>(entity =>
        {
            entity.HasKey(e => e.Recordid).HasName("medicalrecords_pkey");

            entity.ToTable("medicalrecords");

            entity.Property(e => e.Recordid).HasColumnName("recordid");
            entity.Property(e => e.Diseasename)
                .HasMaxLength(100)
                .HasColumnName("diseasename");
            entity.Property(e => e.Enddate).HasColumnName("enddate");
            entity.Property(e => e.Patientid).HasColumnName("patientid");
            entity.Property(e => e.Startdate).HasColumnName("startdate");

            entity.HasOne(d => d.Patient).WithMany(p => p.Medicalrecords)
                .HasForeignKey(d => d.Patientid)
                .HasConstraintName("medicalrecords_patientid_fkey");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Patientid).HasName("patients_pkey");

            entity.ToTable("patients");

            entity.Property(e => e.Patientid).HasColumnName("patientid");
            entity.Property(e => e.Dateofbirth).HasColumnName("dateofbirth");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .HasColumnName("sex");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.Prescriptionid).HasName("prescriptions_pkey");

            entity.ToTable("prescriptions");

            entity.Property(e => e.Prescriptionid).HasColumnName("prescriptionid");
            entity.Property(e => e.Checkupid).HasColumnName("checkupid");
            entity.Property(e => e.Dosage)
                .HasMaxLength(50)
                .HasColumnName("dosage");
            entity.Property(e => e.Medication)
                .HasMaxLength(100)
                .HasColumnName("medication");

            entity.HasOne(d => d.Checkup).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.Checkupid)
                .HasConstraintName("prescriptions_checkupid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
