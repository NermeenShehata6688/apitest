using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class IxpExtraCurricularDto
    {
        public int Id { get; set; }
        public int? IxpId { get; set; }
        public string? Goal { get; set; }
        public string? Strategy { get; set; }
        public int? Indication { get; set; }
        public DateTime? Date { get; set; }
        public string? ExtraCurricularName { get; set; }
        public string? TeacherName { get; set; }
    }
}
