using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class ObjectiveSkillDto
    {
        public int Id { get; set; }
        public int? ObjectiveId { get; set; }
        public int? SkillId { get; set; }
        public int? SkillNumber { get; set; }
    }
}
