using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class StudentSearchDto
    {
        public string? StringSearch { get; set; }
        //public string? NameAr { get; set; }
        //public string? Name { get; set; }
        //public int? Code { get; set; }
        //public int? CivilId { get; set; }
        //public int? PassportNumber { get; set; }
        //public string? Email { get; set; }
        //public string? HomePhone { get; set; }
        public int? NationalityId { get; set; }
        public int? DepartmentId { get; set; }
        public int? TeacherId { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public bool? IsSuspended { get; set; }
        public int Index { get; set; }
        public int PageSize { get; set; }
    }
}
