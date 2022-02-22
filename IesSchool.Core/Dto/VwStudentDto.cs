using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class VwStudentDto
    {
        public string? CityName { get; set; }
        public int Id { get; set; }
        public string? NameAr { get; set; }
        public string? Name { get; set; }
        public int? Code { get; set; }
        public int? CivilId { get; set; }
        public string? Image { get; set; }

        //public byte[]? ImageBinary { get; set; }

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
        public string? NationalityName { get; set; }
        public string? DepartmentName { get; set; }
        public string? TeacherName { get; set; }
        public string? StateName { get; set; }
        public bool? Gender { get; set; }
        public bool? IsActive { get; set; }
        public string? InactiveReason { get; set; }
        public string? FullPath { get; set; }
        public string? ReligionName { get; set; }
        public string? ReligionNameAr { get; set; }
    }
}
