using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    internal class IepParamedicalCreateItpDto
    {
        public int Id { get; set; }
        public int? Iepid { get; set; }
        public int? ParamedicalServiceId { get; set; }
        public int? TherapistId { get; set; }
        public bool? IsItpCreated { get; set; }
        public int? IepYearId { get; set; }
        public int? IepStudentId { get; set; }
        public int? IepTermId { get; set; }
    }
}
