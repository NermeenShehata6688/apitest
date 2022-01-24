using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class ItpStrategy
    {
        public int Id { get; set; }
        public int? ItpId { get; set; }
        public int? ParamedicalStrategyId { get; set; }

        public virtual Itp? Itp { get; set; }
        public virtual ParamedicalStrategy? ParamedicalStrategy { get; set; }
    }
}
