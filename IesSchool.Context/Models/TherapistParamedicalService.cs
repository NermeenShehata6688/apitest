using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class TherapistParamedicalService
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ParamedicalServiceId { get; set; }

        public virtual ParamedicalService? ParamedicalService { get; set; }
        public virtual User? User { get; set; }
    }
}
