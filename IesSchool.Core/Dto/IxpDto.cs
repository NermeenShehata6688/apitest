using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class IxpDto
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

        public string? StudentName { get; set; }
        public string? AcadmicYearName { get; set; }
        public string? TermName { get; set; }
        public string? StudentCodeName { get; set; }
        public string? StudentBirthdayName { get; set; }
        public string? DepartmentName { get; set; }
        public string? HeadOfEducationName { get; set; }
        public string[]? IxpExtraCurricularsName { get; set; }

        public virtual ICollection<IxpExtraCurricularDto>? IxpExtraCurriculars { get; set; }

    }
}
