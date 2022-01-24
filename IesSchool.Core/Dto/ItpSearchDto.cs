using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class ItpSearchDto
    {
        public int? HeadOfDepartment { get; set; }
        public int? HeadOfEducation { get; set; }
        public int? Status { get; set; }
        public bool? IsPublished { get; set; }
        public int? Student_Id { get; set; }
        public int? AcadmicYear_Id { get; set; }
        public int? Term_Id { get; set; }
        public int? Department_Id { get; set; }
        public int? Therapist_Id { get; set; }
        public int? Index { get; set; }
        public int? PageSize { get; set; }
    }
}
