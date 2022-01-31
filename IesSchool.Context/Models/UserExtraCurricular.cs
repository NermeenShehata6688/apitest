using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class UserExtraCurricular
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ExtraCurricularId { get; set; }

        public virtual ExtraCurricular? ExtraCurricular { get; set; }
        public virtual User? User { get; set; }
    }
}
