using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class UserSearchDto
    {
        public string? StringSearch { get; set; }
        public int? NationalityId { get; set; }
        public bool? IsTeacher { get; set; }
        public bool? IsTherapist { get; set; }
        public bool? IsParent { get; set; }
        public bool? IsManager { get; set; }
        public bool? IsHeadofEducation { get; set; }
        public bool? IsOther { get; set; }
        public bool? IsExtraCurricular { get; set; }
        public bool? IsSuspended { get; set; }
        public int? DepartmentId { get; set; }
       // public int[]? DepartmentIdsLst { get; set; }
        public string DepartmentIdsLst { get; set; }
        public int? Index { get; set; }
        public int? PageSize { get; set; }

    }
}
