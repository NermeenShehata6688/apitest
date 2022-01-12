using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class IepProgressReport
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
        public int? GoalShortTermNumber { get; set; }
        public int? GoalLongTermNumber { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }
    }
}
