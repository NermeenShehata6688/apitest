using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IesSchool.Context.Models
{
    public partial class User
    {
        public User()
        {
            EventTeachers = new HashSet<EventTeacher>();
            IepHeadOfDepartmentNavigations = new HashSet<Iep>();
            IepHeadOfEducationNavigations = new HashSet<Iep>();
            IepProgressReportHeadOfEducations = new HashSet<IepProgressReport>();
            IepProgressReportTeachers = new HashSet<IepProgressReport>();
            IepTeachers = new HashSet<Iep>();
            StudentTherapists = new HashSet<StudentTherapist>();
            Students = new HashSet<Student>();
            TherapistParamedicalServices = new HashSet<TherapistParamedicalService>();
            UserAssistants = new HashSet<UserAssistant>();
            UserAttachments = new HashSet<UserAttachment>();
            LogComments = new HashSet<LogComment>();
        }

        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int? NationalityId { get; set; }
        public bool? IsTeacher { get; set; }
        public bool? IsTherapist { get; set; }
        public bool? IsParent { get; set; }
        public bool? IsManager { get; set; }
        public bool? IsHeadofEducation { get; set; }
        public bool? IsOther { get; set; }
        public int? RoomNumber { get; set; }
        public bool? IsExtraCurricular { get; set; }
        public byte[]? ImageBinary { get; set; }
        public string? Image { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public bool? IsSuspended { get; set; }
        public int? DepartmentId { get; set; }
        public string? ParentUserName { get; set; }
        public string? ParentPassword { get; set; }
        public bool? Gender { get; set; }

        public virtual Department? Department { get; set; }
        public virtual Country? Nationality { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<EventTeacher> EventTeachers { get; set; }
        public virtual ICollection<Iep> IepHeadOfDepartmentNavigations { get; set; }
        public virtual ICollection<Iep> IepHeadOfEducationNavigations { get; set; }
        public virtual ICollection<IepProgressReport> IepProgressReportHeadOfEducations { get; set; }
        public virtual ICollection<IepProgressReport> IepProgressReportTeachers { get; set; }
        public virtual ICollection<Iep> IepTeachers { get; set; }
        public virtual ICollection<StudentTherapist> StudentTherapists { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<TherapistParamedicalService> TherapistParamedicalServices { get; set; }
        public virtual ICollection<UserAssistant> UserAssistants { get; set; }
        public virtual ICollection<UserAttachment> UserAttachments { get; set; }
        public virtual ICollection<LogComment> LogComments { get; set; }
    }
}
