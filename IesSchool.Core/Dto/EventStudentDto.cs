using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class EventStudentDto
    {
        public int Id { get; set; }
        public int? EventId { get; set; }
        public int? StudentId { get; set; }
        public string? EventName { get; set; }
        public string? StudentName { get; set; }
        public string? StudentNameAr { get; set; }
       // public virtual ICollection<EventStudentFileDto>? EventStudentFiles { get; set; }

    }
}
