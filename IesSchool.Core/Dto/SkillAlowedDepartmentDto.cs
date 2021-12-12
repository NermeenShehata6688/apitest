using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class SkillAlowedDepartmentDto
    {
        public int Id { get; set; }
        public int? DepartmentId { get; set; }
        public int? SkillId { get; set; }
    }
}
