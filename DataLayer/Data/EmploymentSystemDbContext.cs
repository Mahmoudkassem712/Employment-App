using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Data;

public partial class EmploymentSystemDbContext : DbContext
{
    private string _ConnectionString { get; set; }
    public EmploymentSystemDbContext(string ConnectionString)
    {
        _ConnectionString = ConnectionString;
    }

    public EmploymentSystemDbContext(DbContextOptions<EmploymentSystemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApplicantVacancy> ApplicantVacancies { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    public virtual DbSet<Vacancy> Vacancies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_ConnectionString); 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicantVacancy>(entity =>
        {
            entity.Property(e => e.ApplyingDate).HasColumnType("date");

            entity.HasOne(d => d.Vacancy).WithMany(p => p.ApplicantVacancies)
                .HasForeignKey(d => d.VacancyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicantVacancies_Vacancy");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FullName).HasMaxLength(256);

        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.ToTable("UserType");

            entity.Property(e => e.UserTypeName)
                .HasMaxLength(50)
                .HasColumnName("UserTypeName");
        });

        modelBuilder.Entity<Vacancy>(entity =>
        {
            entity.ToTable("Vacancy");

            entity.Property(e => e.ExpiryDate).HasColumnType("date");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
