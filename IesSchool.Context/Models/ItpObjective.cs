using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class ItpObjective
    {
        public int Id { get; set; }
        public int? ItpId { get; set; }
        public string? ObjectiveNote { get; set; }
        public DateTime? Date { get; set; }
        public string? ResourcesRequired { get; set; }
        public int? EvaluationId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Itp? Itp { get; set; }
    }
}
