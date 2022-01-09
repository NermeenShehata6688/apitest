using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class ProgressReportParamedicalDto
    {
        public int Id { get; set; }
        public int? ProgressReportId { get; set; }
        public int? ParamedicalServiceId { get; set; }
        public string? Comment { get; set; }
    }
}
