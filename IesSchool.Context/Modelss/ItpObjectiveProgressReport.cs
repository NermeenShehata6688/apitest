using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class ItpObjectiveProgressReport
    {
        public int Id { get; set; }
        public int? ItpProgressReportId { get; set; }
        public int? ItpObjectiveId { get; set; }
        public string? Comment { get; set; }

        public virtual ItpObjective? ItpObjective { get; set; }
        public virtual ItpProgressReport? ItpProgressReport { get; set; }
    }
}
