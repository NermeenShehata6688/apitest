using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class IepProgressReportDto
    {
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

        public string? StudentCode { get; set; }
        public string? StudentName { get; set; }
        public string? AcadmicYearName { get; set; }
        public string? TermName { get; set; }
        public int? StrandsCount { get; set; }
        public int? ParamedicalCount { get; set; }
        public int? ExtraCount { get; set; }


        public virtual ICollection<ProgressReportExtraCurricularDto>? ProgressReportExtraCurriculars { get; set; }
        public virtual ICollection<ProgressReportParamedicalDto>? ProgressReportParamedicals { get; set; }
        public virtual ICollection<ProgressReportStrandDto>? ProgressReportStrands { get; set; }
    }
}
