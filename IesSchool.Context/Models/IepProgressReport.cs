using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class IepProgressReport
    {
        public IepProgressReport()
        {
            ProgressReportExtraCurriculars = new HashSet<ProgressReportExtraCurricular>();
            ProgressReportParamedicals = new HashSet<ProgressReportParamedical>();
            ProgressReportStrands = new HashSet<ProgressReportStrand>();
        }

        public int Id { get; set; }
        public int? IepId { get; set; }
        public int? StudentId { get; set; }
        public int? AcadmicYearId { get; set; }
        public int? TermId { get; set; }
        public DateTime? Date { get; set; }
        public string? GeneralComment { get; set; }
        public string? OtherComment { get; set; }
        public int? TeacherId { get; set; }
        public int? HeadOfEducationId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }

        public virtual AcadmicYear? AcadmicYear { get; set; }
        public virtual User? HeadOfEducation { get; set; }
        public virtual Iep? Iep { get; set; }
        public virtual Student? Student { get; set; }
        public virtual User? Teacher { get; set; }
        public virtual Term? Term { get; set; }
        public virtual ICollection<ProgressReportExtraCurricular> ProgressReportExtraCurriculars { get; set; }
        public virtual ICollection<ProgressReportParamedical> ProgressReportParamedicals { get; set; }
        public virtual ICollection<ProgressReportStrand> ProgressReportStrands { get; set; }
    }
}
