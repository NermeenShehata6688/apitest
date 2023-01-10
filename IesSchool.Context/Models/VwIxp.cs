using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class VwIxp
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public int? AcadmicYearId { get; set; }
        public int? TermId { get; set; }
        public DateTime? DateOfPreparation { get; set; }
        public DateTime? LastDateOfReview { get; set; }
        public int? HeadOfDepartmentId { get; set; }
        public int? HeadOfEducationId { get; set; }
        public bool? ParentsInvolvedInSettingUpSuggestions { get; set; }
        public bool? ProgressReport { get; set; }
        public bool? Others { get; set; }
        public int? Status { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public string? FooterNotes { get; set; }
        public string? StudentNotes { get; set; }
        public bool? IsPublished { get; set; }
        public int? ExtraCurricularId { get; set; }
        public int? ExTeacherId { get; set; }
        public int? StudentsIdJ { get; set; }
        public string? NameAr { get; set; }
        public string? Name { get; set; }
        public int? Code { get; set; }
        public int? AcadmicYearsIdJ { get; set; }
        public string? AcadmicYearsName { get; set; }
        public int? TermIdJ { get; set; }
        public string? TermName { get; set; }
        public int? DepartmentIdJ { get; set; }
        public string? DepartmentName { get; set; }
        public int? ExtraCurricularIdJ { get; set; }
        public string? ExtraCurricularName { get; set; }
        public string? ExtraCurricularNameAr { get; set; }
        public int? UserIdJ { get; set; }
        public string? UserCode { get; set; }
        public string? UserNamea { get; set; }
    }
}
