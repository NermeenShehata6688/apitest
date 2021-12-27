using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class VwSkillDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameAr { get; set; }
        public int? Code { get; set; }
        public int? DisplayOrder { get; set; }
        public int? Level { get; set; }
        public int? StrandId { get; set; }
        public string? StrandName { get; set; }
        public string? AreaName { get; set; }
        public string? AreaNameAr { get; set; }
        public string? StrandNameAr { get; set; }
    }
}
