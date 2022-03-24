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
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public bool? IsDeleted { get; set; }

        public string? ExtraCurricularName { get; set; }
        public string? ExTeacherName { get; set; }

    }
}
