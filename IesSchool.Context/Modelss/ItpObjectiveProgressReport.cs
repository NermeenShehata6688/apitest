﻿using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class ItpObjectiveProgressReport
    {
        public int Id { get; set; }
        public int? ItpProgressReportId { get; set; }
        public int? ItpObjectiveId { get; set; }
        public string? Comment { get; set; }

        public virtual ItpGoalObjective? ItpObjective { get; set; }
    }
}
