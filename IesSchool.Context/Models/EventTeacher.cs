using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class EventTeacher
    {
        public int Id { get; set; }
        public int? EventId { get; set; }
        public int? TeacherId { get; set; }

        public virtual Event? Event { get; set; }
        public virtual User? Teacher { get; set; }
    }
}
