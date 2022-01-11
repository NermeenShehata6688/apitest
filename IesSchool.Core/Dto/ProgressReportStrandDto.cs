using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class ProgressReportStrandDto
    {
        public int Id { get; set; }
        public int? ProgressReportId { get; set; }
        public int? StrandId { get; set; }
        public int? FirstTermPercentage { get; set; }
        public int? SecondTermPercentage { get; set; }
        public string? Comment { get; set; }
        public string? AreaName { get; set; }
        public string? StrandName { get; set; }
    }
}
