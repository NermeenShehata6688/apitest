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
        public IPaginate<VwStudent> AllStudents { get; set; }
        public IPaginate<ParamedicalService> AllParamedicalServices { get; set; }
        public IPaginate<User> AllTherapist { get; set; }
        public IPaginate<AcadmicYear> AllAcadmicYears { get; set; }
        public IPaginate<Term> AllTerms { get; set; }
        public IPaginate<User> AllHeadOfEducations { get; set; }
        public IPaginate<TherapistParamedicalService> TherapistParamedicalService { get; set; }
        public IPaginate<Department> AllDepartments { get; set; }
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
