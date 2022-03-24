using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
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
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual User? Therapist { get; set; }
        public virtual Iep? Iep { get; set; }
        public virtual ParamedicalService? ParamedicalService { get; set; }
        public virtual ICollection<Itp> Itps { get; set; }

    }
}
