using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
namespace IesSchool.Context.Models
{
    public partial class iesContext  :IdentityDbContext<AspNetUser, AspNetRole, int>
    {
      

        public iesContext(DbContextOptions<iesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Calender> Calenders { get; set; } = null!;
        public virtual DbSet<CalenderCategory> CalenderCategories { get; set; } = null!;
        public virtual DbSet<AcadmicYear> AcadmicYears { get; set; } = null!;
        public virtual DbSet<Activity> Activities { get; set; } = null!;
        public virtual DbSet<ApplicationGroup> ApplicationGroups { get; set; } = null!;
        public virtual DbSet<ApplicationGroupRole> ApplicationGroupRoles { get; set; } = null!;
        public virtual DbSet<ApplicationUserGroup> ApplicationUserGroups { get; set; } = null!;
        public virtual DbSet<Area> Areas { get; set; } = null!;
        //public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        //public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        //public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        //public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        //public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
           public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; } = null!;
        //public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Assistant> Assistants { get; set; } = null!;
        public virtual DbSet<AttachmentType> AttachmentTypes { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<EventAttachement> EventAttachements { get; set; } = null!;
        public virtual DbSet<EventAttachmentBinary> EventAttachmentBinaries { get; set; } = null!;
        public virtual DbSet<EventStudent> EventStudents { get; set; } = null!;
        public virtual DbSet<LogComment> LogComments { get; set; } = null!;
        public virtual DbSet<EventStudentFile> EventStudentFiles { get; set; } = null!;
        public virtual DbSet<EventStudentFileBinary> EventStudentFileBinaries { get; set; } = null!;
        public virtual DbSet<EventTeacher> EventTeachers { get; set; } = null!;
        public virtual DbSet<EventType> EventTypes { get; set; } = null!;
        public virtual DbSet<ExtraCurricular> ExtraCurriculars { get; set; } = null!;
        public virtual DbSet<Goal> Goals { get; set; } = null!;
        public virtual DbSet<Iep> Ieps { get; set; } = null!;
        public virtual DbSet<IepAssistant> IepAssistants { get; set; } = null!;
        public virtual DbSet<IepExtraCurricular> IepExtraCurriculars { get; set; } = null!;
        public virtual DbSet<IepParamedicalService> IepParamedicalServices { get; set; } = null!;
        public virtual DbSet<IepProgressReport> IepProgressReports { get; set; } = null!;
        public virtual DbSet<Objective> Objectives { get; set; } = null!;
        public virtual DbSet<ObjectiveEvaluationProcess> ObjectiveEvaluationProcesses { get; set; } = null!;
        public virtual DbSet<ObjectiveSkill> ObjectiveSkills { get; set; } = null!;
        public virtual DbSet<ParamedicalService> ParamedicalServices { get; set; } = null!;
        public virtual DbSet<Phone> Phones { get; set; } = null!;
        public virtual DbSet<Program> Programs { get; set; } = null!;
        public virtual DbSet<ProgressReportExtraCurricular> ProgressReportExtraCurriculars { get; set; } = null!;
        public virtual DbSet<ProgressReportParamedical> ProgressReportParamedicals { get; set; } = null!;
        public virtual DbSet<ProgressReportStrand> ProgressReportStrands { get; set; } = null!;
        public virtual DbSet<Religion> Religions { get; set; } = null!;
        public virtual DbSet<Setting> Settings { get; set; } = null!;
        public virtual DbSet<Skill> Skills { get; set; } = null!;
        public virtual DbSet<SkillAlowedDepartment> SkillAlowedDepartments { get; set; } = null!;
        public virtual DbSet<SkillEvaluation> SkillEvaluations { get; set; } = null!;
        public virtual DbSet<State> States { get; set; } = null!;
        public virtual DbSet<Strand> Strands { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<StudentExtraTeacher> StudentExtraTeachers { get; set; } = null!;
        public virtual DbSet<StudentAttachment> StudentAttachments { get; set; } = null!;
        public virtual DbSet<StudentAttachmentBinary> StudentAttachmentBinaries { get; set; } = null!;
        public virtual DbSet<StudentHistoricalSkill> StudentHistoricalSkills { get; set; } = null!;
        public virtual DbSet<StudentTherapist> StudentTherapists { get; set; } = null!;
        public virtual DbSet<Term> Terms { get; set; } = null!;
        public virtual DbSet<Itp> Itps { get; set; } = null!;
        public virtual DbSet<ItpGoal> ItpGoals { get; set; } = null!;
        public virtual DbSet<ItpGoalObjective> ItpGoalObjectives { get; set; } = null!;
        public virtual DbSet<ItpGoalObjectiveActivity> ItpGoalObjectiveActivities { get; set; } = null!;
        public virtual DbSet<ItpObjectiveProgressReport> ItpObjectiveProgressReports { get; set; } = null!;
        public virtual DbSet<ItpProgressReport> ItpProgressReports { get; set; } = null!;
        public virtual DbSet<ItpStrategy> ItpStrategies { get; set; } = null!;
        public virtual DbSet<Ixp> Ixps { get; set; } = null!;
        public virtual DbSet<IxpExtraCurricular> IxpExtraCurriculars { get; set; } = null!;
        public virtual DbSet<TherapistParamedicalService> TherapistParamedicalServices { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserExtraCurricular> UserExtraCurriculars { get; set; } = null!;
        public virtual DbSet<UserAssistant> UserAssistants { get; set; } = null!;
        public virtual DbSet<UserAttachment> UserAttachments { get; set; } = null!;
        public virtual DbSet<UserAttachmentBinary> UserAttachmentBinaries { get; set; } = null!;
        public virtual DbSet<VwAssistant> VwAssistants { get; set; } = null!;
        public virtual DbSet<VwIep> VwIeps { get; set; } = null!;
        public virtual DbSet<VwStudent> VwStudents { get; set; } = null!;
        public virtual DbSet<VwUser> VwUsers { get; set; } = null!;
        public virtual DbSet<WorkCategory> WorkCategories { get; set; } = null!;
        public virtual DbSet<VwSkill> VwSkills { get; set; } = null!;
        public virtual DbSet<VwIxp> VwIxps { get; set; } = null!;

      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<AspNetUserRole>(entity =>
            //{
            //    entity.HasKey(e => new { e.UserId, e.RoleId });
            //    entity.ToTable("AspNetUserRoles");
            //    entity.Property(e => e.Code)
            //        .HasMaxLength(10)
            //        .IsUnicode(false)
            //        .HasColumnName("code")
            //        .IsFixedLength();

            //    //entity.HasOne(d => d.Role)
            //    //    .WithMany(p => p.AspNetUserRoles)
            //    //    .HasForeignKey(d => d.RoleId);

            //    //entity.HasOne(d => d.User)
            //    //    .WithMany(p => p.AspNetUserRoles)
            //    //    .HasForeignKey(d => d.UserId);
            //});
            modelBuilder.Entity<LogComment>(entity =>
            {
                entity.ToTable("LogComment");

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.LogDate).HasColumnType("datetime");
                entity.HasOne(d => d.Iep)
                   .WithMany(p => p.LogComments)
                   .HasForeignKey(d => d.IepId)
                   .HasConstraintName("FK_LogComment_IEP");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.LogComments)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_LogComment_Students");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LogComments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_LogComment_User");
            });
            modelBuilder.Entity<VwSkill>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Skills");

                entity.Property(e => e.AreaName).HasMaxLength(255);

                entity.Property(e => e.AreaNameAr).HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Level).HasColumnName("level");
                //entity.Property(e => e.SkillNumber).HasColumnName("level"); 
                entity.Property(e => e.NameAr).HasColumnName("Name_Ar");

                entity.Property(e => e.StrandName).HasMaxLength(255);

                entity.Property(e => e.StrandNameAr).HasMaxLength(255);
            });
            modelBuilder.Entity<VwIxp>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Vw_Ixp");

                entity.Property(e => e.AcadmicYearsName).HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DateOfPreparation).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.DepartmentName).HasMaxLength(255);

                entity.Property(e => e.LastDateOfReview).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.NameAr).HasMaxLength(500);

                entity.Property(e => e.TermName).HasMaxLength(255);

                entity.Property(e => e.UserCode).HasMaxLength(255);

                entity.Property(e => e.UserNamea).HasMaxLength(1000);
            });
            modelBuilder.Entity<AcadmicYear>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Objective)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.ObjectiveId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Activities_Objective");
            });

            modelBuilder.Entity<ApplicationGroup>(entity =>
            {
                entity.ToTable("ApplicationGroup");

                entity.Property(e => e.Name).HasMaxLength(128);
            });

            modelBuilder.Entity<ApplicationGroupRole>(entity =>
            {
                entity.HasKey(e => new { e.ApplicationGroupId, e.ApplicationRoleId });

                entity.ToTable("ApplicationGroupRole");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.HasOne(d => d.ApplicationGroup)
                    .WithMany(p => p.ApplicationGroupRoles)
                    .HasForeignKey(d => d.ApplicationGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationGroupRole_ApplicationGroup");

                entity.HasOne(d => d.ApplicationRole)
                    .WithMany(p => p.ApplicationGroupRoles)
                    .HasForeignKey(d => d.ApplicationRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationGroupRole_AspNetRoles");
            });

            modelBuilder.Entity<ApplicationUserGroup>(entity =>
            {
                entity.HasKey(e => new { e.ApplicationUserId, e.ApplicationGroupId });

                entity.ToTable("ApplicationUserGroup");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.HasOne(d => d.ApplicationGroup)
                    .WithMany(p => p.ApplicationUserGroups)
                    .HasForeignKey(d => d.ApplicationGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationUserGroup_ApplicationGroup");

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.ApplicationUserGroups)
                    .HasForeignKey(d => d.ApplicationUserId)
                    .HasConstraintName("FK_ApplicationUserGroup_AspNetUsers");
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.ToTable("Area");

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.NameAr)
                    .HasMaxLength(255)
                    .HasColumnName("Name_Ar");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.Areas)
                    .HasForeignKey(d => d.ProgramId)
                    .HasConstraintName("FK_Area_Program");
            });

            //modelBuilder.Entity<AspNetRole>(entity =>
            //{
            //    entity.Property(e => e.Name).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedName).HasMaxLength(256);
            //});

            //modelBuilder.Entity<AspNetRoleClaim>(entity =>
            //{
            //    entity.HasOne(d => d.Role)
            //        .WithMany(p => p.AspNetRoleClaims)
            //        .HasForeignKey(d => d.RoleId);
            //});

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                //entity.Property(e => e.Id).ValueGeneratedNever();

                //entity.Property(e => e.Email).HasMaxLength(256);

                //entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                //entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                //entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.AspNetUser)
                    .HasForeignKey<AspNetUser>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AspNetUsers_User");
            });

            //modelBuilder.Entity<AspNetUserClaim>(entity =>
            //{
            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserClaims)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUserLogin>(entity =>
            //{
            //    entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserLogins)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUserRole>(entity =>
            //{
            //    entity.HasNoKey();

            //    entity.HasOne(d => d.Role)
            //        .WithMany()
            //        .HasForeignKey(d => d.RoleId);

            //    entity.HasOne(d => d.User)
            //        .WithMany()
            //        .HasForeignKey(d => d.UserId);
            //});

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.LoginProvider).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Assistant>(entity =>
            {
                entity.ToTable("Assistant");

                entity.Property(e => e.BirthDay).HasColumnType("datetime");

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.Mobile).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(1000);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Assistants)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Assistant_Department");

                entity.HasOne(d => d.Nationality)
                    .WithMany(p => p.Assistants)
                    .HasForeignKey(d => d.NationalityId)
                    .HasConstraintName("FK_Assistant_Country");
            });

            modelBuilder.Entity<AttachmentType>(entity =>
            {
                entity.ToTable("AttachmentType");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.NameAr).HasMaxLength(500);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_City_State");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event");

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Event_Department");

                entity.HasOne(d => d.EventType)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.EventTypeId)
                    .HasConstraintName("FK_Event_EventType");
            });

            modelBuilder.Entity<EventAttachement>(entity =>
            {
                entity.ToTable("EventAttachement");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(1000);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventAttachements)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_EventAttachement_Event");
            });

            modelBuilder.Entity<EventAttachmentBinary>(entity =>
            {
                entity.ToTable("EventAttachmentBinary");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.EventAttachmentBinary)
                    .HasForeignKey<EventAttachmentBinary>(d => d.Id)
                    .HasConstraintName("FK_EventAttachmentBinary_EventAttachement");
            });

            modelBuilder.Entity<EventStudent>(entity =>
            {
                entity.ToTable("Event_Student");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventStudents)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Event_Student_Event");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.EventStudents)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Event_Student_Students");
            });

            modelBuilder.Entity<EventStudentFile>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.EventStudent)
                    .WithMany(p => p.EventStudentFiles)
                    .HasForeignKey(d => d.EventStudentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_EventStudentFiles_Event_Student1");
            });

            modelBuilder.Entity<EventStudentFileBinary>(entity =>
            {
                entity.ToTable("EventStudentFileBinary");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.EventStudentFileBinary)
                    .HasForeignKey<EventStudentFileBinary>(d => d.Id)
                    .HasConstraintName("FK_EventStudentFileBinary_EventStudentFiles");
            });

            modelBuilder.Entity<EventTeacher>(entity =>
            {
                entity.ToTable("Event_Teacher");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventTeachers)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Event_Teacher_Event");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.EventTeachers)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_Event_Teacher_User");
            });

            modelBuilder.Entity<EventType>(entity =>
            {
                entity.ToTable("EventType");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NameAr).HasMaxLength(256);
            });

            modelBuilder.Entity<ExtraCurricular>(entity =>
            {
                entity.ToTable("ExtraCurricular");

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.NameAr).HasColumnName("Name_Ar");
            });

            modelBuilder.Entity<Goal>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Iepid).HasColumnName("IEPId");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Goals)
                    .HasForeignKey(d => d.AreaId)
                    .HasConstraintName("FK_Goals_Area");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.Goals)
                    .HasForeignKey(d => d.ProgramId)
                    .HasConstraintName("FK_Goals_Program");

                entity.HasOne(d => d.Iep)
                    .WithMany(p => p.Goals)
                    .HasForeignKey(d => d.Iepid)
                    .HasConstraintName("FK_Goals_IEP");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.Goals)
                    .HasForeignKey(d => d.SkillId)
                    .HasConstraintName("FK_Goals_Skill");

                entity.HasOne(d => d.Strand)
                    .WithMany(p => p.Goals)
                    .HasForeignKey(d => d.StrandId)
                    .HasConstraintName("FK_Goals_Strand");
            });

            modelBuilder.Entity<Iep>(entity =>
            {
                entity.ToTable("IEP");

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DateOfPreparation).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.LastDateOfReview).HasColumnType("datetime");

                entity.HasOne(d => d.AcadmicYear)
                    .WithMany(p => p.Ieps)
                    .HasForeignKey(d => d.AcadmicYearId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IEP_AcadmicYears");

                entity.HasOne(d => d.HeadOfDepartmentNavigation)
                    .WithMany(p => p.IepHeadOfDepartmentNavigations)
                    .HasForeignKey(d => d.HeadOfDepartment)
                    .HasConstraintName("FK_IEP_HeadOfDepartmentUser");

                entity.HasOne(d => d.HeadOfEducationNavigation)
                    .WithMany(p => p.IepHeadOfEducationNavigations)
                    .HasForeignKey(d => d.HeadOfEducation)
                    .HasConstraintName("FK_IEP_HeadOfEducationUser");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Ieps)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IEP_Students");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.IepTeachers)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_IEP_TeacherIdUser");

                entity.HasOne(d => d.Term)
                    .WithMany(p => p.Ieps)
                    .HasForeignKey(d => d.TermId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IEP_Term");
            });

            modelBuilder.Entity<IepAssistant>(entity =>
            {
                entity.ToTable("IepAssistant");

                entity.Property(e => e.Iepid).HasColumnName("IEPId");

                entity.HasOne(d => d.Assistant)
                    .WithMany(p => p.IepAssistants)
                    .HasForeignKey(d => d.AssistantId)
                    .HasConstraintName("FK_IepAssistant_Assistant");

                entity.HasOne(d => d.Iep)
                    .WithMany(p => p.IepAssistants)
                    .HasForeignKey(d => d.Iepid)
                    .HasConstraintName("FK_IepAssistant_IEP");
            });
            modelBuilder.Entity<IepExtraCurricular>(entity =>
            {
                entity.ToTable("IEP_ExtraCurricular");

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Iepid).HasColumnName("IEPId");

                entity.HasOne(d => d.ExTeacher)
                    .WithMany(p => p.IepExtraCurriculars)
                    .HasForeignKey(d => d.ExTeacherId)
                    .HasConstraintName("FK_IEP_ExtraCurricular_User");

                entity.HasOne(d => d.ExtraCurricular)
                    .WithMany(p => p.IepExtraCurriculars)
                    .HasForeignKey(d => d.ExtraCurricularId)
                    .HasConstraintName("FK_IEP_ExtraCurricular_ExtraCurricular");

                entity.HasOne(d => d.Iep)
                    .WithMany(p => p.IepExtraCurriculars)
                    .HasForeignKey(d => d.Iepid)
                    .HasConstraintName("FK_IEP_ExtraCurricular_IEP");
            });

            modelBuilder.Entity<IepParamedicalService>(entity =>
            {
                entity.ToTable("IEP_ParamedicalService");

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Iepid).HasColumnName("IEPId");

                entity.HasOne(d => d.Iep)
                    .WithMany(p => p.IepParamedicalServices)
                    .HasForeignKey(d => d.Iepid)
                    .HasConstraintName("FK_IEP_ParamedicalService_IEP");

                entity.HasOne(d => d.ParamedicalService)
                    .WithMany(p => p.IepParamedicalServices)
                    .HasForeignKey(d => d.ParamedicalServiceId)
                    .HasConstraintName("FK_IEP_ParamedicalService_ParamedicalService");

                entity.HasOne(d => d.Therapist)
                    .WithMany(p => p.IepParamedicalServices)
                    .HasForeignKey(d => d.TherapistId)
                    .HasConstraintName("FK_IEP_ParamedicalService_Therapist");
            });

            modelBuilder.Entity<IepProgressReport>(entity =>
            {
                entity.ToTable("IepProgressReport");

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.AcadmicYear)
                    .WithMany(p => p.IepProgressReports)
                    .HasForeignKey(d => d.AcadmicYearId)
                    .HasConstraintName("FK_IepProgressReport_AcadmicYears");

                entity.HasOne(d => d.HeadOfEducation)
                    .WithMany(p => p.IepProgressReportHeadOfEducations)
                    .HasForeignKey(d => d.HeadOfEducationId)
                    .HasConstraintName("FK_IepProgressReport_HeadOfEducation");

                entity.HasOne(d => d.Iep)
                    .WithMany(p => p.IepProgressReports)
                    .HasForeignKey(d => d.IepId)
                    .HasConstraintName("FK_IepProgressReport_IEP");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.IepProgressReports)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_IepProgressReport_Students");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.IepProgressReportTeachers)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_IepProgressReport_Teacher");

                entity.HasOne(d => d.Term)
                    .WithMany(p => p.IepProgressReports)
                    .HasForeignKey(d => d.TermId)
                    .HasConstraintName("FK_IepProgressReport_Term");


            });
            modelBuilder.Entity<Itp>(entity =>
            {
                entity.ToTable("ITP");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DateOfPreparation).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.LastDateOfReview).HasColumnType("datetime");

                entity.HasOne(d => d.AcadmicYear)
                    .WithMany(p => p.Itps)
                    .HasForeignKey(d => d.AcadmicYearId)
                    .HasConstraintName("FK_ITP_AcadmicYears");

                entity.HasOne(d => d.HeadOfDepartment)
                    .WithMany(p => p.ItpHeadOfDepartments)
                    .HasForeignKey(d => d.HeadOfDepartmentId)
                    .HasConstraintName("FK_ITP_HeadOfDepartment");

                entity.HasOne(d => d.HeadOfEducation)
                    .WithMany(p => p.ItpHeadOfEducations)
                    .HasForeignKey(d => d.HeadOfEducationId)
                    .HasConstraintName("FK_ITP_HeadOfEducation");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Itp)
                    .HasForeignKey<Itp>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ITP_IEP_ParamedicalService");

                entity.HasOne(d => d.ParamedicalService)
                    .WithMany(p => p.Itps)
                    .HasForeignKey(d => d.ParamedicalServiceId)
                    .HasConstraintName("FK_ITP_ParamedicalService");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Itps)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_ITP_Students");

                entity.HasOne(d => d.Term)
                    .WithMany(p => p.Itps)
                    .HasForeignKey(d => d.TermId)
                    .HasConstraintName("FK_ITP_Term");

                entity.HasOne(d => d.TherapistDepartment)
                    .WithMany(p => p.Itps)
                    .HasForeignKey(d => d.TherapistDepartmentId)
                    .HasConstraintName("FK_ITP_Department");

                entity.HasOne(d => d.Therapist)
                    .WithMany(p => p.ItpTherapists)
                    .HasForeignKey(d => d.TherapistId)
                    .HasConstraintName("FK_ITP_Therapist");
            });

            modelBuilder.Entity<ItpGoal>(entity =>
            {
                entity.ToTable("ITP_Goal");

                entity.HasOne(d => d.Itp)
                    .WithMany(p => p.ItpGoals)
                    .HasForeignKey(d => d.ItpId)
                    .HasConstraintName("FK_ITP_Goal_ITP_Goal");
            });

            modelBuilder.Entity<ItpGoalObjective>(entity =>
            {
                entity.ToTable("ITP_GoalObjective");

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.ItpGoal)
                    .WithMany(p => p.ItpGoalObjectives)
                    .HasForeignKey(d => d.ItpGoalId)
                    .HasConstraintName("FK_ITP_GoalObjective_ITP_Goal");

                entity.HasOne(d => d.Itp)
                    .WithMany(p => p.ItpGoalObjectives)
                    .HasForeignKey(d => d.ItpId)
                    .HasConstraintName("FK_ITP_Objective_ITP");
            });


            modelBuilder.Entity<ItpGoalObjectiveActivity>(entity =>
            {
                entity.ToTable("ITP_GoalObjectiveActivity");

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.ItpGoalObjective)
                    .WithMany(p => p.ItpGoalObjectiveActivities)
                    .HasForeignKey(d => d.ItpGoalObjectiveId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ITP_GoalObjectiveActivity_ITP_GoalObjective");
            });

            modelBuilder.Entity<ItpObjectiveProgressReport>(entity =>
            {
                entity.ToTable("ITP_ObjectiveProgressReport");

                entity.HasOne(d => d.ItpObjective)
                    .WithMany(p => p.ItpObjectiveProgressReports)
                    .HasForeignKey(d => d.ItpObjectiveId)
                    .HasConstraintName("FK_ITP_ObjectiveProgressReport_ITP_Objective");

                entity.HasOne(d => d.ItpProgressReport)
                    .WithMany(p => p.ItpObjectiveProgressReports)
                    .HasForeignKey(d => d.ItpProgressReportId)
                    .HasConstraintName("FK_ITP_ObjectiveProgressReport_ITP_ProgressReport");
            });

            modelBuilder.Entity<ItpProgressReport>(entity =>
            {
                entity.ToTable("ITP_ProgressReport");

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.AcadmicYear)
                    .WithMany(p => p.ItpProgressReports)
                    .HasForeignKey(d => d.AcadmicYearId)
                    .HasConstraintName("FK_ITP_ProgressReport_AcadmicYears");

                entity.HasOne(d => d.HeadOfEducation)
                    .WithMany(p => p.ItpProgressReportHeadOfEducations)
                    .HasForeignKey(d => d.HeadOfEducationId)
                    .HasConstraintName("FK_ITP_ProgressReport_HeadOfEducation");

                entity.HasOne(d => d.Itp)
                    .WithMany(p => p.ItpProgressReports)
                    .HasForeignKey(d => d.ItpId)
                    .HasConstraintName("FK_ITP_ProgressReport_ITP");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.ItpProgressReports)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_ITP_ProgressReport_Students");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.ItpProgressReportTeachers)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_ITP_ProgressReport_Teacher");

                entity.HasOne(d => d.Term)
                    .WithMany(p => p.ItpProgressReports)
                    .HasForeignKey(d => d.TermId)
                    .HasConstraintName("FK_ITP_ProgressReport_Term");

                entity.HasOne(d => d.Therapist)
                    .WithMany(p => p.ItpProgressReportTherapists)
                    .HasForeignKey(d => d.TherapistId)
                    .HasConstraintName("FK_ITP_ProgressReport_Therapist");

                entity.HasOne(d => d.ParamedicalService)
                  .WithMany(p => p.ItpProgressReports)
                  .HasForeignKey(d => d.ParamedicalServiceId)
                  .HasConstraintName("FK_ITP_ProgressReport_ParamedicalService");
            });
            modelBuilder.Entity<ItpStrategy>(entity =>
            {
                entity.ToTable("ITP_Strategy");

                entity.HasOne(d => d.Itp)
                    .WithMany(p => p.ItpStrategies)
                    .HasForeignKey(d => d.ItpId)
                    .HasConstraintName("FK_ITP_Strategy_ITP");

                entity.HasOne(d => d.ParamedicalStrategy)
                    .WithMany(p => p.ItpStrategies)
                    .HasForeignKey(d => d.ParamedicalStrategyId)
                    .HasConstraintName("FK_ITP_Strategy_Paramedical_Strategy");
            });
            modelBuilder.Entity<Ixp>(entity =>
            {
                entity.ToTable("IXP");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DateOfPreparation).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.LastDateOfReview).HasColumnType("datetime");

                entity.HasOne(d => d.AcadmicYear)
                    .WithMany(p => p.Ixps)
                    .HasForeignKey(d => d.AcadmicYearId)
                    .HasConstraintName("FK_IXP_AcadmicYears");

                entity.HasOne(d => d.ExTeacher)
                    .WithMany(p => p.IxpExTeachers)
                    .HasForeignKey(d => d.ExTeacherId)
                    .HasConstraintName("FK_IXP_ExTeacher");

                entity.HasOne(d => d.ExtraCurricular)
                    .WithMany(p => p.Ixps)
                    .HasForeignKey(d => d.ExtraCurricularId)
                    .HasConstraintName("FK_IXP_ExtraCurricular");

                entity.HasOne(d => d.HeadOfDepartment)
                    .WithMany(p => p.IxpHeadOfDepartments)
                    .HasForeignKey(d => d.HeadOfDepartmentId)
                    .HasConstraintName("FK_IXP_HeadOfDepartment");

                entity.HasOne(d => d.HeadOfEducation)
                    .WithMany(p => p.IxpHeadOfEducations)
                    .HasForeignKey(d => d.HeadOfEducationId)
                    .HasConstraintName("FK_IXP_HeadOfEducation");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Ixp)
                    .HasForeignKey<Ixp>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IXP_IEP_ExtraCurricular1");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Ixps)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_IXP_Students");

                entity.HasOne(d => d.Term)
                    .WithMany(p => p.Ixps)
                    .HasForeignKey(d => d.TermId)
                    .HasConstraintName("FK_IXP_Term");
            });

            modelBuilder.Entity<IxpExtraCurricular>(entity =>
            {
                entity.ToTable("IXP_ExtraCurricular");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Ixp)
                    .WithMany(p => p.IxpExtraCurriculars)
                    .HasForeignKey(d => d.IxpId)
                    .HasConstraintName("FK_IXP_ExtraCurricular_IXP");
            });

            modelBuilder.Entity<Objective>(entity =>
            {
                entity.ToTable("Objective");

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Goal)
                    .WithMany(p => p.Objectives)
                    .HasForeignKey(d => d.GoalId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Objective_Goals");
            });

            modelBuilder.Entity<ObjectiveEvaluationProcess>(entity =>
            {
                entity.ToTable("Objective_EvaluationProcess");

                entity.HasOne(d => d.Objective)
                    .WithMany(p => p.ObjectiveEvaluationProcesses)
                    .HasForeignKey(d => d.ObjectiveId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Objective_EvaluationProcess_Objective");

                entity.HasOne(d => d.SkillEvaluation)
                    .WithMany(p => p.ObjectiveEvaluationProcesses)
                    .HasForeignKey(d => d.SkillEvaluationId)
                    .HasConstraintName("FK_Objective_EvaluationProcess_SkillEvaluation");
            });

            modelBuilder.Entity<ObjectiveSkill>(entity =>
            {
                entity.ToTable("Objective_Skill");

                entity.HasOne(d => d.Objective)
                    .WithMany(p => p.ObjectiveSkills)
                    .HasForeignKey(d => d.ObjectiveId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Objective_Skill_Objective");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.ObjectiveSkills)
                    .HasForeignKey(d => d.SkillId)
                    .HasConstraintName("FK_Objective_Skill_Skill");
            });

            modelBuilder.Entity<ParamedicalService>(entity =>
            {
                entity.ToTable("ParamedicalService");

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.NameAr).HasColumnName("Name_Ar");
            });
            modelBuilder.Entity<ParamedicalStrategy>(entity =>
            {
                entity.ToTable("Paramedical_Strategy");

                entity.HasOne(d => d.ParamedicalService)
                    .WithMany(p => p.ParamedicalStrategies)
                    .HasForeignKey(d => d.ParamedicalServiceId)
                    .HasConstraintName("FK_Para_Strategy_ParamedicalService");
            });

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.ToTable("Phone");

                entity.Property(e => e.Owner).HasMaxLength(500);

                entity.Property(e => e.Phone1)
                    .HasMaxLength(255)
                    .HasColumnName("Phone");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Phones)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Phone_Students");
            });

            modelBuilder.Entity<Program>(entity =>
            {
                entity.ToTable("Program");

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<ProgressReportExtraCurricular>(entity =>
            {
                entity.ToTable("ProgressReportExtraCurricular");

                entity.HasOne(d => d.ExtraCurricular)
                    .WithMany(p => p.ProgressReportExtraCurriculars)
                    .HasForeignKey(d => d.ExtraCurricularId)
                    .HasConstraintName("FK_ProgressReportExtraCurricular_ExtraCurricular");

                entity.HasOne(d => d.ProgressReport)
                    .WithMany(p => p.ProgressReportExtraCurriculars)
                    .HasForeignKey(d => d.ProgressReportId)
                    .HasConstraintName("FK_ProgressReportExtraCurricular_IepProgressReport");
            });

            modelBuilder.Entity<ProgressReportParamedical>(entity =>
            {
                entity.ToTable("ProgressReportParamedical");

                entity.HasOne(d => d.IepParamedicalSercive)
                    .WithMany(p => p.ProgressReportParamedicals)
                    .HasForeignKey(d => d.IepParamedicalSerciveId)
                    .HasConstraintName("FK_ProgressReportParamedical_IEP_ParamedicalService");

                entity.HasOne(d => d.ParamedicalService)
                    .WithMany(p => p.ProgressReportParamedicals)
                    .HasForeignKey(d => d.ParamedicalServiceId)
                    .HasConstraintName("FK_ProgressReportParamedical_ParamedicalService");

                entity.HasOne(d => d.ProgressReport)
                    .WithMany(p => p.ProgressReportParamedicals)
                    .HasForeignKey(d => d.ProgressReportId)
                    .HasConstraintName("FK_ProgressReportParamedical_IepProgressReport");
            });

            modelBuilder.Entity<ProgressReportStrand>(entity =>
            {
                entity.ToTable("ProgressReportStrand");

                entity.HasOne(d => d.ProgressReport)
                    .WithMany(p => p.ProgressReportStrands)
                    .HasForeignKey(d => d.ProgressReportId)
                    .HasConstraintName("FK_ProgressReportStrand_IepProgressReport");

                entity.HasOne(d => d.Strand)
                    .WithMany(p => p.ProgressReportStrands)
                    .HasForeignKey(d => d.StrandId)
                    .HasConstraintName("FK_ProgressReportStrand_Strand");
            });

            modelBuilder.Entity<Religion>(entity =>
            {
                entity.ToTable("Religion");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.NameAr).HasMaxLength(250);
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.ToTable("Setting");

                entity.HasOne(d => d.CurrentTerm)
                    .WithMany(p => p.Settings)
                    .HasForeignKey(d => d.CurrentTermId)
                    .HasConstraintName("FK_Setting_Term");

                entity.HasOne(d => d.CurrentYear)
                    .WithMany(p => p.Settings)
                    .HasForeignKey(d => d.CurrentYearId)
                    .HasConstraintName("FK_Setting_AcadmicYears");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.ToTable("Skill");

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.NameAr).HasColumnName("Name_Ar");

                entity.HasOne(d => d.Strand)
                    .WithMany(p => p.Skills)
                    .HasForeignKey(d => d.StrandId)
                    .HasConstraintName("FK_Skill_Strand");
            });

            modelBuilder.Entity<SkillAlowedDepartment>(entity =>
            {
                entity.ToTable("Skill_AlowedDepartment");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.SkillAlowedDepartments)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Skill_AlowedDepartment_Department");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.SkillAlowedDepartments)
                    .HasForeignKey(d => d.SkillId)
                    .HasConstraintName("FK_Skill_AlowedDepartment_Skill");
            });

            modelBuilder.Entity<SkillEvaluation>(entity =>
            {
                entity.ToTable("SkillEvaluation");

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.NameAr).HasColumnName("Name_Ar");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("State");

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_State_Country");
            });

            modelBuilder.Entity<Strand>(entity =>
            {
                entity.ToTable("Strand");

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.NameAr)
                    .HasMaxLength(255)
                    .HasColumnName("Name_Ar");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Strands)
                    .HasForeignKey(d => d.AreaId)
                    .HasConstraintName("FK_Strand_Area");
            });

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

                entity.HasOne(d => d.BirthCountry)
                    .WithMany(p => p.StudentBirthCountries)
                    .HasForeignKey(d => d.BirthCountryId)
                    .HasConstraintName("FK_Students_BirthCountry");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Students_City");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Students_Department");

                entity.HasOne(d => d.FatherNationality)
                    .WithMany(p => p.StudentFatherNationalities)
                    .HasForeignKey(d => d.FatherNationalityId)
                    .HasConstraintName("FK_Students_FatherNationality");

                entity.HasOne(d => d.MotherNationality)
                    .WithMany(p => p.StudentMotherNationalities)
                    .HasForeignKey(d => d.MotherNationalityId)
                    .HasConstraintName("FK_Students_MotherNationality");

                entity.HasOne(d => d.Nationality)
                    .WithMany(p => p.StudentNationalities)
                    .HasForeignKey(d => d.NationalityId)
                    .HasConstraintName("FK_Students_Nationality");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Students_State");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.StudentTeachers)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_Students_Teacher");
                entity.HasOne(d => d.Parent)
                   .WithMany(p => p.StudentParents)
                   .HasForeignKey(d => d.ParentId)
                   .HasConstraintName("FK_Students_Parent");
                entity.HasOne(d => d.Religion)
                   .WithMany(p => p.Students)
                   .HasForeignKey(d => d.ReligionId)
                   .HasConstraintName("FK_Students_Religion");
            });
            modelBuilder.Entity<StudentExtraTeacher>(entity =>
            {
                entity.ToTable("Student_ExtraTeacher");

                entity.HasOne(d => d.ExtraTeacher)
                    .WithMany(p => p.StudentExtraTeachers)
                    .HasForeignKey(d => d.ExtraTeacherId)
                    .HasConstraintName("FK_Student_ExtraTeacher_User");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentExtraTeachers)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Student_ExtraTeacher_Students");
            });

            modelBuilder.Entity<StudentAttachment>(entity =>
            {
                entity.ToTable("StudentAttachment");

                entity.Property(e => e.IssuedIn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.ValidTill).HasColumnType("datetime");

                entity.HasOne(d => d.AttachmentType)
                    .WithMany(p => p.StudentAttachments)
                    .HasForeignKey(d => d.AttachmentTypeId)
                    .HasConstraintName("FK_StudentAttachment_AttachmentType");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentAttachments)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_StudentAttachment_Students");
            });

            modelBuilder.Entity<StudentAttachmentBinary>(entity =>
            {
                entity.ToTable("StudentAttachmentBinary");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasColumnName("code");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.StudentAttachmentBinary)
                    .HasForeignKey<StudentAttachmentBinary>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentAttachmentBinary_StudentAttachment");
            });

            modelBuilder.Entity<StudentHistoricalSkill>(entity =>
            {
                entity.HasOne(d => d.HistoricalSkilldNavigation)
                    .WithMany(p => p.StudentHistoricalSkills)
                    .HasForeignKey(d => d.HistoricalSkilld)
                    .HasConstraintName("FK_StudentHistoricalSkills_Skill");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentHistoricalSkills)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_StudentHistoricalSkills_Students");
            });

            modelBuilder.Entity<StudentTherapist>(entity =>
            {
                entity.ToTable("Student_Therapist");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentTherapists)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Student_Therapist_Students");

                entity.HasOne(d => d.Therapist)
                    .WithMany(p => p.StudentTherapists)
                    .HasForeignKey(d => d.TherapistId)
                    .HasConstraintName("FK_Student_Therapist_User");
            });

            modelBuilder.Entity<Term>(entity =>
            {
                entity.ToTable("Term");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<TherapistParamedicalService>(entity =>
            {
                entity.ToTable("TherapistParamedicalService");

                entity.HasOne(d => d.ParamedicalService)
                    .WithMany(p => p.TherapistParamedicalServices)
                    .HasForeignKey(d => d.ParamedicalServiceId)
                    .HasConstraintName("FK_TherapistParamedicalService_ParamedicalService");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TherapistParamedicalServices)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_TherapistParamedicalService_User");
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

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_User_Department");

                entity.HasOne(d => d.Nationality)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.NationalityId)
                    .HasConstraintName("FK_User_Country");
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

            modelBuilder.Entity<UserAssistant>(entity =>
            {
                entity.ToTable("User_Assistant");

                entity.HasOne(d => d.Assistant)
                    .WithMany(p => p.UserAssistants)
                    .HasForeignKey(d => d.AssistantId)
                    .HasConstraintName("FK_User_Assistant_Assistant");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAssistants)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_User_Assistant_User");
            });

            modelBuilder.Entity<UserAttachment>(entity =>
            {
                entity.ToTable("UserAttachment");

                entity.Property(e => e.IssuedIn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.ValidTill).HasColumnType("datetime");

                entity.HasOne(d => d.AttachmentType)
                    .WithMany(p => p.UserAttachments)
                    .HasForeignKey(d => d.AttachmentTypeId)
                    .HasConstraintName("FK_UserAttachment_AttachmentType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAttachments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserAttachment_User");
            });

            modelBuilder.Entity<UserAttachmentBinary>(entity =>
            {
                entity.ToTable("UserAttachmentBinary");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.UserAttachmentBinary)
                    .HasForeignKey<UserAttachmentBinary>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAttachmentBinary_UserAttachment");
            });

            modelBuilder.Entity<VwAssistant>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Vw_Assistants");

                entity.Property(e => e.BirthDay).HasColumnType("datetime");

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.DepartmentName).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(1000);

                entity.Property(e => e.NationalityName).HasMaxLength(255);
            });

            modelBuilder.Entity<VwIep>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("Vw_Ieps");

                entity.Property(e => e.AcadmicYear_Id).HasColumnName("AcadmicYear_Id");

                entity.Property(e => e.AcadmicYearName).HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.DateOfPreparation).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Department_Id).HasColumnName("Department_Id");

                entity.Property(e => e.DepartmentName).HasMaxLength(255);

                entity.Property(e => e.LastDateOfReview).HasColumnType("datetime");

                entity.Property(e => e.Student_Id).HasColumnName("Student_Id");

                entity.Property(e => e.StudentName).HasMaxLength(500);

                entity.Property(e => e.StudentNameAr).HasMaxLength(500);

                entity.Property(e => e.Teacher_Id).HasColumnName("Teacher_Id");

                entity.Property(e => e.TeacherName).HasMaxLength(1000);

                entity.Property(e => e.Term_Id).HasColumnName("Term_Id");

                entity.Property(e => e.TermName).HasMaxLength(255);
            });

            modelBuilder.Entity<VwStudent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Vw_Students");

                entity.Property(e => e.Block).HasMaxLength(255);

                entity.Property(e => e.BuildingNumber).HasMaxLength(255);

                entity.Property(e => e.CertificateIssueDate).HasColumnType("datetime");

                entity.Property(e => e.CertificateIssueLocation).HasMaxLength(500);

                entity.Property(e => e.CityName).HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.DepartmentName).HasMaxLength(255);

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

                entity.Property(e => e.NationalityName).HasMaxLength(255);

                entity.Property(e => e.StateName).HasMaxLength(255);

                entity.Property(e => e.Street).HasMaxLength(255);

                entity.Property(e => e.TeacherName).HasMaxLength(1000);
            });

            modelBuilder.Entity<VwUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Vw_Users");

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(500);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.DepartmentName).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(1000);

                entity.Property(e => e.NationalityName).HasMaxLength(255);

                entity.Property(e => e.ParentPassword).HasMaxLength(128);

                entity.Property(e => e.ParentUserName).HasMaxLength(128);
            });

            modelBuilder.Entity<WorkCategory>(entity =>
            {
                entity.ToTable("WorkCategory");

                entity.Property(e => e.Name).HasMaxLength(1000);

                entity.Property(e => e.NameAr).HasMaxLength(1000);
            });

            //OnModelCreatingPartial(modelBuilder);
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    } 



    //public partial class iesIdentityContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    //{
    //    public iesIdentityContext()
    //    {
    //    }

    //    public iesIdentityContext(DbContextOptions<iesIdentityContext> options)
    //        : base(options)
    //    {
    //    }
    //}
}
