﻿
namespace IesSchool.Core.Dto
{
    public class UserDto
    {
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
        public DateTime? BirthDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public bool? IsSuspended { get; set; }
        public int? DepartmentId { get; set; }
        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? ParentUserName { get; set; }
        public string? ParentPassword { get; set; }
        public string? FullPath { get; set; }
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? UserRoles { get; set; }
        public string? UserPassword { get; set; }
        public int[]? StudentsIdsForParent { get; set; }
        public bool? IsActive { get; set; }
        public string? ParentCivilId { get; set; }
        public string? DeviceToken { get; set; }
        public string? StudentsNames { get; set; }

        public virtual ICollection<UserAssistantDto>? UserAssistants { get; set; }
        public virtual ICollection<StudentTherapistDto>? StudentTherapists { get; set; }
        public virtual ICollection<TherapistParamedicalServiceDto>? TherapistParamedicalServices { get; set; }
        public virtual ICollection<UserAttachmentDto>? UserAttachments { get; set; }

        public virtual ICollection<UserExtraCurricularDto>? UserExtraCurriculars { get; set; }
       // public virtual ICollection<StudentExtraTeacherDto>? StudentExtraTeachers { get; set; }

       // public virtual ICollection<StudentParentDto>? StudentParents { get; set; }

    }
}
