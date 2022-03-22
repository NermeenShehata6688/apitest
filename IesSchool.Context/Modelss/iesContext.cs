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

        public virtual DbSet<IepParamedicalService> IepParamedicalServices { get; set; } = null!;
        public virtual DbSet<Itp> Itps { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=192.168.8.132\\SQL2016;Database=ies;User Id=sa; Password=P@ss@123@@");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IepParamedicalService>(entity =>
            {
                entity.ToTable("IEP_ParamedicalService");

                entity.Property(e => e.Iepid).HasColumnName("IEPId");

                entity.HasOne(d => d.Therapist)
                    .WithMany(p => p.IepParamedicalServices)
                    .HasForeignKey(d => d.TherapistId)
                    .HasConstraintName("FK_IEP_ParamedicalService_User");
            });

            modelBuilder.Entity<Itp>(entity =>
            {
                entity.ToTable("ITP");

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DateOfPreparation).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.IepparamedicalServiceId).HasColumnName("IEPParamedicalServiceId");

                entity.Property(e => e.LastDateOfReview).HasColumnType("datetime");

                entity.HasOne(d => d.HeadOfDepartment)
                    .WithMany(p => p.ItpHeadOfDepartments)
                    .HasForeignKey(d => d.HeadOfDepartmentId)
                    .HasConstraintName("FK_ITP_HeadOfDepartment");

                entity.HasOne(d => d.HeadOfEducation)
                    .WithMany(p => p.ItpHeadOfEducations)
                    .HasForeignKey(d => d.HeadOfEducationId)
                    .HasConstraintName("FK_ITP_HeadOfEducation");

                entity.HasOne(d => d.IepparamedicalService)
                    .WithMany(p => p.Itps)
                    .HasForeignKey(d => d.IepparamedicalServiceId)
                    .HasConstraintName("FK_ITP_IEP_ParamedicalService");

                entity.HasOne(d => d.Therapist)
                    .WithMany(p => p.ItpTherapists)
                    .HasForeignKey(d => d.TherapistId)
                    .HasConstraintName("FK_ITP_Therapist");
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

                entity.Property(e => e.Phone1).HasMaxLength(255);

                entity.Property(e => e.Phone2).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
