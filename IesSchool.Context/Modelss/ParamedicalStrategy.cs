using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class ParamedicalStrategy
    {
        public ParamedicalStrategy()
        {
            ItpStrategies = new HashSet<ItpStrategy>();
        }

        public int Id { get; set; }
        public int? ParamedicalServiceId { get; set; }
        public string? StrategyName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ParamedicalService? ParamedicalService { get; set; }
        public virtual ICollection<ItpStrategy> ItpStrategies { get; set; }
    }
}
