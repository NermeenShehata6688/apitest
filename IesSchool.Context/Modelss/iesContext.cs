using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IesSchool.Context.Modelss
{
    public partial class iesContext : DbContext
    {
        public iesContext()
        {
        }

        public iesContext(DbContextOptions<iesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=192.168.8.188;Database=ies;User Id=sa; Password=P@ss@123@@");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Block).HasMaxLength(255);

                entity.Property(e => e.BuildingNumber).HasMaxLength(255);

                entity.Property(e => e.CertificateIssueDate).HasColumnType("datetime");

                entity.Property(e => e.CertificateIssueLocation).HasMaxLength(500);

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.EmergencyPhone).HasMaxLength(255);

                entity.Property(e => e.FatherPhone).HasMaxLength(255);

                entity.Property(e => e.FatherStatus).HasMaxLength(255);

                entity.Property(e => e.FatherWorkLocation).HasMaxLength(500);

                entity.Property(e => e.HomePhone).HasMaxLength(255);

                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.JoinDate).HasColumnType("datetime");

                entity.Property(e => e.Level).HasMaxLength(255);

                entity.Property(e => e.MotherPhone).HasMaxLength(255);

                entity.Property(e => e.MotherStatus).HasMaxLength(255);

                entity.Property(e => e.MotherWorkLocation).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.NameAr).HasMaxLength(500);

                entity.Property(e => e.Street).HasMaxLength(255);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.StudentParents)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Students_Parent");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.StudentTeachers)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_Students_Teacher");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(1000);

                entity.Property(e => e.ParentPassword).HasMaxLength(128);

                entity.Property(e => e.ParentUserName).HasMaxLength(128);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
