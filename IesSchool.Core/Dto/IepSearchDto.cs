using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class IepSearchDto
    {
        public int? HeadOfDepartment { get; set; }
        public int? HeadOfEducation { get; set; }
        public int? Status { get; set; }
        public bool? IsPublished { get; set; }
        public int? Index { get; set; }
        public int? PageSize { get; set; }
        public int? Student_Id { get; set; }
        public int? AcadmicYear_Id { get; set; }
        public int? Department_Id { get; set; }
        public int? Teacher_Id { get; set; }
        public int? Term_Id { get; set; }
        //public int? AcadmicYearId { get; set; }
        //public int? TermId { get; set; }
        //public int? TeacherId { get; set; }
        //public int? DepartmentId { get; set; }
        //public int? RoomNumber { get; set; }
        //public string? StudentNameAr { get; set; }
        //public string? StudentName { get; set; }
        public string? StudentCode { get; set; }
    }
}
