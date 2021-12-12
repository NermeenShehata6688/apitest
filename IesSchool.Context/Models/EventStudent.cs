using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class EventStudent
    {
        public EventStudent()
        {
            EventStudentFiles = new HashSet<EventStudentFile>();
        }

        public int Id { get; set; }
        public int? EventId { get; set; }
        public int? StudentId { get; set; }

        public virtual Event? Event { get; set; }
        public virtual Student? Student { get; set; }
        public virtual ICollection<EventStudentFile> EventStudentFiles { get; set; }
    }
}
