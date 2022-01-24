using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class IepParamedicalService
    {
        public int Id { get; set; }
        public int? Iepid { get; set; }
        public int? ParamedicalServiceId { get; set; }

        public virtual Iep? Iep { get; set; }
        public virtual ParamedicalService? ParamedicalService { get; set; }
    }
}
