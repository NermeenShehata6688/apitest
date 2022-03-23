using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class ParamedicalService
    {
        public ParamedicalService()
        {
            IepParamedicalServices = new HashSet<IepParamedicalService>();
            ItpProgressReports = new HashSet<ItpProgressReport>();
            Itps = new HashSet<Itp>();
            ParamedicalStrategies = new HashSet<ParamedicalStrategy>();
            ProgressReportParamedicals = new HashSet<ProgressReportParamedical>();
            TherapistParamedicalServices = new HashSet<TherapistParamedicalService>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameAr { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }

        public virtual ICollection<IepParamedicalService> IepParamedicalServices { get; set; }
        public virtual ICollection<ItpProgressReport> ItpProgressReports { get; set; }
        public virtual ICollection<Itp> Itps { get; set; }
        public virtual ICollection<ParamedicalStrategy> ParamedicalStrategies { get; set; }
        public virtual ICollection<ProgressReportParamedical> ProgressReportParamedicals { get; set; }
        public virtual ICollection<TherapistParamedicalService> TherapistParamedicalServices { get; set; }
    }
}
