using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class Setting
    {
        public int Id { get; set; }
        public int? CurrentYearId { get; set; }
        public int? CurrentTermId { get; set; }
        public int? LongTermPercent { get; set; }
        public int? ShortTermPercent { get; set; }

        public virtual Term? CurrentTerm { get; set; }
        public virtual AcadmicYear? CurrentYear { get; set; }
    }
}
