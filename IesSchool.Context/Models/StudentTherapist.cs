using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class StudentTherapist
    {
        public int Id { get; set; }
        public int? TherapistId { get; set; }
        public int? StudentId { get; set; }

        public virtual Student? Student { get; set; }
        public virtual User? Therapist { get; set; }
    }
}
