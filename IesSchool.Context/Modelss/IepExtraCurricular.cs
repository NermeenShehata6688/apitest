using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class IepExtraCurricular
    {
        public IepExtraCurricular()
        {
            Ixps = new HashSet<Ixp>();
        }

        public int Id { get; set; }
        public int? Iepid { get; set; }
        public int? ExtraCurricularId { get; set; }
        public int? ExTeacherId { get; set; }
        public bool? IsIxpCreated { get; set; }

        public virtual User? ExTeacher { get; set; }
        public virtual ExtraCurricular? ExtraCurricular { get; set; }
        public virtual Iep? Iep { get; set; }
        public virtual ICollection<Ixp> Ixps { get; set; }
    }
}
