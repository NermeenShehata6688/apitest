using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class IepExtraCurricular
    {
        public IepExtraCurricular()
        {
            ProgressReportExtraCurriculars = new HashSet<ProgressReportExtraCurricular>();
        }

        public int Id { get; set; }
        public int? Iepid { get; set; }
        public int? ExtraCurricularId { get; set; }
        public int? ExTeacherId { get; set; }
        public bool? IsIxpCreated { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual User? ExTeacher { get; set; }
        public virtual ExtraCurricular? ExtraCurricular { get; set; }
        public virtual Iep? Iep { get; set; }
        public virtual Ixp Ixp { get; set; } = null!;
        public virtual ICollection<ProgressReportExtraCurricular> ProgressReportExtraCurriculars { get; set; }
    }
}
