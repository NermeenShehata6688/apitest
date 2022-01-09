using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class ProgressReportExtraCurricularDto
    {
        public int Id { get; set; }
        public int? ProgressReportId { get; set; }
        public int? ExtraCurricularId { get; set; }
        public string? Comment { get; set; }
    }
}
