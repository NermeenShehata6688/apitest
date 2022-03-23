using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class IepParamedicalService
    {
        public IepParamedicalService()
        {
            Itps = new HashSet<Itp>();
        }

        public int Id { get; set; }
        public int? Iepid { get; set; }
        public int? ParamedicalServiceId { get; set; }
        public int? TherapistId { get; set; }
        public bool? IsItpCreated { get; set; }

        public virtual Iep? Iep { get; set; }
        public virtual ParamedicalService? ParamedicalService { get; set; }
        public virtual User? Therapist { get; set; }
        public virtual ICollection<Itp> Itps { get; set; }
    }
}
