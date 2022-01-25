using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class ItpObjectiveProgressReportDto
    {
        public int Id { get; set; }
        public int? ItpProgressReportId { get; set; }
        public int? ItpObjectiveId { get; set; }
        public string? Comment { get; set; }
        public string? ItpObjectiveNote { get; set; }
    }
}
