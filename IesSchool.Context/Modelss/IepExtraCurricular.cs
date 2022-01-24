using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class IepExtraCurricular
    {
        public int Id { get; set; }
        public int? Iepid { get; set; }
        public int? ExtraCurricularId { get; set; }

        public virtual ExtraCurricular? ExtraCurricular { get; set; }
        public virtual Iep? Iep { get; set; }
    }
}
