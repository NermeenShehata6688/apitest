using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class IepExtraCurricularDto
    {
        public int Id { get; set; }
        public int? Iepid { get; set; }
        public int? ExtraCurricularId { get; set; }
        public int? ExTeacherId { get; set; }
        public bool? IsIxpCreated { get; set; }
        public string? ExtraCurricularName { get; set; }
    }
}
