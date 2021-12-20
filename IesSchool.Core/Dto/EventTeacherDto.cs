using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class EventTeacherDto
    {
        public int Id { get; set; }
        public int? EventId { get; set; }
        public int? TeacherId { get; set; }
    }
}
