using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class UserExtraCurricularDto
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ExtraCurricularId { get; set; }
    }
}
