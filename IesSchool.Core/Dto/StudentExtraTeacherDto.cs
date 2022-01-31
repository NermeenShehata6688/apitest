using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class StudentExtraTeacherDto
    {
        public int Id { get; set; }
        public int? ExtraTeacherId { get; set; }
        public int? StudentId { get; set; }
    }
}
