using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class ParamedicalStrategyDto
    {
        public int Id { get; set; }
        public int? ParamedicalServiceId { get; set; }
        public string? StrategyName { get; set; }
    }
}
