using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class ProgressReportExtraCurricular
    {
        public int Id { get; set; }
        public int? ProgressReportId { get; set; }
        public int? ExtraCurricularId { get; set; }
        public string? Comment { get; set; }

        public virtual ExtraCurricular? ExtraCurricular { get; set; }
        public virtual IepProgressReport? ProgressReport { get; set; }
    }
}
