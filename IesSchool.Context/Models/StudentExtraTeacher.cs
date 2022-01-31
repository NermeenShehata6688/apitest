using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class StudentExtraTeacher
    {
        public int Id { get; set; }
        public int? ExtraTeacherId { get; set; }
        public int? StudentId { get; set; }

        public virtual User? ExtraTeacher { get; set; }
        public virtual Student? Student { get; set; }
    }
}
