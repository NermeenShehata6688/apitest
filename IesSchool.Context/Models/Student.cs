using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IesSchool.Context.Models
{
    public partial class Student
    {
        public Student()
        {
            EventStudents = new HashSet<EventStudent>();
            Ieps = new HashSet<Iep>();
            Phones = new HashSet<Phone>();
            StudentAttachments = new HashSet<StudentAttachment>();
            StudentHistoricalSkills = new HashSet<StudentHistoricalSkill>();
            StudentTherapists = new HashSet<StudentTherapist>();
        }

        public int Id { get; set; }
        public string? NameAr { get; set; }
        public string? Name { get; set; }
        public int? Code { get; set; }
        public int? CivilId { get; set; }
        public string? Image { get; set; }
        public byte[]? ImageBinary { get; set; }
        public int? NationalityId { get; set; }
        public int? PassportNumber { get; set; }
        public int? ReligionId { get; set; }
        public string? Email { get; set; }
        public int? DepartmentId { get; set; }
        public string? StudentNotes { get; set; }
        public string? PaymentNotes { get; set; }
        public string? Block { get; set; }
        public string? Street { get; set; }
        public int? TeacherId { get; set; }
        public string? BuildingNumber { get; set; }
        public string? FatherPhone { get; set; }
        public string? MotherPhone { get; set; }
        public string? HomePhone { get; set; }
        public string? EmergencyPhone { get; set; }
        public int? FatherNationalityId { get; set; }
        public string? FatherStatus { get; set; }
        public string? FatherWorkLocation { get; set; }
        public int? FatherWorkCategory { get; set; }
        public int? MotherNationalityId { get; set; }
        public string? MotherStatus { get; set; }
        public string? MotherWorkLocation { get; set; }
        public int? MotherWorkCategory { get; set; }
        public int? Certificate { get; set; }
        public DateTime? CertificateIssueDate { get; set; }
        public string? CertificateIssueLocation { get; set; }
        public bool? IsSuspended { get; set; }
        public string? SuspensionReason { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? BirthCountryId { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public int? ParentId { get; set; }
        public int? TermType { get; set; }
        public string? DisabilityNotes { get; set; }
        public int? MinistryNumber { get; set; }
        public string? Level { get; set; }
        public DateTime? JoinDate { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Boulevard { get; set; }
        public bool? Gender { get; set; }
        public bool? IsActive { get; set; }
        public string? InactiveReason { get; set; }

        public virtual Country? BirthCountry { get; set; }
        public virtual City? City { get; set; }
        public virtual Department? Department { get; set; }
        public virtual Country? FatherNationality { get; set; }
        public virtual Country? MotherNationality { get; set; }
        public virtual Country? Nationality { get; set; }
        public virtual State? State { get; set; }
        public virtual User? Teacher { get; set; }
        public virtual ICollection<EventStudent> EventStudents { get; set; }
        public virtual ICollection<Iep> Ieps { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
        public virtual ICollection<StudentAttachment> StudentAttachments { get; set; }
        public virtual ICollection<StudentHistoricalSkill> StudentHistoricalSkills { get; set; }
        public virtual ICollection<StudentTherapist> StudentTherapists { get; set; }
    }
}
