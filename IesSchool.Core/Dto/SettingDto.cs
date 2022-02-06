using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class SettingDto
    {
        public int Id { get; set; }
        public int? CurrentYearId { get; set; }
        public int? CurrentTermId { get; set; }
        public int? LongTermPercent { get; set; }
        public int? ShortTermPercent { get; set; }

        public string? CurrentYearName { get; set; }
        public string? CurrentTermNamed { get; set; }

    }
}
