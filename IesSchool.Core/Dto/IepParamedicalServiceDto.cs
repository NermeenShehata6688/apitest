using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class IepParamedicalServiceDto
    {
        public int Id { get; set; }
        public int? Iepid { get; set; }
        public int? ParamedicalServiceId { get; set; }
        public string? ParamedicalServiceName { get; set; }
    }
}
