using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class LogComment
    {
        public int Id { get; set; }
        public int? IepId { get; set; }
        public int? StudentId { get; set; }
        public int? UserId { get; set; }
        public DateTime? LogDate { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public virtual Iep? Iep { get; set; }
        public virtual Student? Student { get; set; }
        public virtual User? User { get; set; }
    }
}
