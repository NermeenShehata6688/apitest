using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
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

        public virtual ParamedicalService? ParamedicalService { get; set; }
        public virtual ICollection<ItpStrategy> ItpStrategies { get; set; }
    }
}
