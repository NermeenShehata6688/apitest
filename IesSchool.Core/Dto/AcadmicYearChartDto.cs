using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class AcadmicYearChartDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? IsCurrent { get; set; }
        public int IepCount { get; set; }
        public int ItpCount { get; set; }
        public int IxpCount { get; set; }

    }
}
