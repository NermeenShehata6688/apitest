using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class Activity
    {
        public int Id { get; set; }
        public string? Deatils { get; set; }
        public DateTime? Date { get; set; }
        public string? CreatedBy { get; set; }
        public int? ObjectiveId { get; set; }
        public int? Evaluation { get; set; }

        public virtual Objective? Objective { get; set; }
    }
}
