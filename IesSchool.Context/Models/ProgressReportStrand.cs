using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class ProgressReportStrand
    {
        public int Id { get; set; }
        public int? ProgressReportId { get; set; }
        public int? StrandId { get; set; }
        public int? FirstTermPercentage { get; set; }
        public int? SecondTermPercentage { get; set; }
        public string? Comment { get; set; }
        public int? GoalShortTermNumber { get; set; }
        public int? GoalLongTermNumber { get; set; }

        public virtual IepProgressReport? ProgressReport { get; set; }
        public virtual Strand? Strand { get; set; }
    }
}
