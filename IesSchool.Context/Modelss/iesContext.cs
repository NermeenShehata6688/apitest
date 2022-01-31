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

        public virtual DbSet<ExtraCurricular> ExtraCurriculars { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserExtraCurricular> UserExtraCurriculars { get; set; } = null!;

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
            modelBuilder.Entity<ExtraCurricular>(entity =>
            {
                entity.ToTable("ExtraCurricular");

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.NameAr).HasColumnName("Name_Ar");
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

            modelBuilder.Entity<UserExtraCurricular>(entity =>
            {
                entity.ToTable("User_ExtraCurricular");

                entity.HasOne(d => d.ExtraCurricular)
                    .WithMany(p => p.UserExtraCurriculars)
                    .HasForeignKey(d => d.ExtraCurricularId)
                    .HasConstraintName("FK_User_ExtraCurricular_User_ExtraCurricular");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserExtraCurriculars)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_User_ExtraCurricular_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
