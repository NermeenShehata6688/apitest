using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class Ixp
    {
        public Ixp()
        {
            IxpExtraCurriculars = new HashSet<IxpExtraCurricular>();
        }

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

        public virtual AcadmicYear? AcadmicYear { get; set; }
        public virtual User? ExTeacher { get; set; }
        public virtual ExtraCurricular? ExtraCurricular { get; set; }
        public virtual User? HeadOfDepartment { get; set; }
        public virtual User? HeadOfEducation { get; set; }
        public virtual IepExtraCurricular IdNavigation { get; set; } = null!;
        public virtual Student? Student { get; set; }
        public virtual Term? Term { get; set; }
        public virtual ICollection<IxpExtraCurricular> IxpExtraCurriculars { get; set; }
    }
}
