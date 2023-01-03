using IesSchool.Context.Models;
using IesSchool.InfraStructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class ItpHelper
    {
        public PaginateDto<VwStudent> AllStudents { get; set; }
        public PaginateDto<ParamedicalService> AllParamedicalServices { get; set; }
        public PaginateDto<User> AllTherapist { get; set; }
        public PaginateDto<AcadmicYear> AllAcadmicYears { get; set; }
        public PaginateDto<Term> AllTerms { get; set; }
        public PaginateDto<User> AllHeadOfEducations { get; set; }
        public PaginateDto<TherapistParamedicalService> TherapistParamedicalService { get; set; }
        public PaginateDto<Department> AllDepartments { get; set; }
    }
    public class ItpHelperDto
    {
        public PaginateDto<VwStudentDto> AllStudents { get; set; }
        public PaginateDto<ParamedicalServiceDto> AllParamedicalServices { get; set; }
        public PaginateDto<UserDto> AllTherapist { get; set; }
        public PaginateDto<AcadmicYearDto> AllAcadmicYears { get; set; }
        public PaginateDto<TermDto> AllTerms { get; set; }
        public PaginateDto<UserDto> AllHeadOfEducations { get; set; }
       public PaginateDto<TherapistParamedicalServiceDto> TherapistParamedicalService { get; set; }
        public PaginateDto<DepartmentDto> AllDepartments { get; set; }
    }
}
