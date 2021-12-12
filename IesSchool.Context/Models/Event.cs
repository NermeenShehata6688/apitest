using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class Event
    {
        public Event()
        {
            EventAttachements = new HashSet<EventAttachement>();
            EventStudents = new HashSet<EventStudent>();
            EventTeachers = new HashSet<EventTeacher>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public int? EventTypeId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }
        public bool? IsPublished { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public virtual Department? Department { get; set; }
        public virtual EventType? EventType { get; set; }
        public virtual ICollection<EventAttachement> EventAttachements { get; set; }
        public virtual ICollection<EventStudent> EventStudents { get; set; }
        public virtual ICollection<EventTeacher> EventTeachers { get; set; }
    }
}
