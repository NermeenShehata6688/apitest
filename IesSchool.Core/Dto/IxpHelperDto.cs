using IesSchool.Context.Models;
using IesSchool.InfraStructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{

    public class IxpHelper
    {
        public IPaginate<VwStudent> AllStudents { get; set; }
        public IPaginate<ExtraCurricular> AllExtraCurriculars { get; set; }
        public IPaginate<User> AllExTeacher { get; set; }
        public IPaginate<AcadmicYear> AllAcadmicYears { get; set; }
        public IPaginate<Term> AllTerms { get; set; }
        public IPaginate<User> AllHeadOfEducations { get; set; }
        public IPaginate<Department> AllDepartments { get; set; }
    }

    public class IxpHelperDto
    {
        public PaginateDto<VwStudentDto> AllStudents { get; set; }
        public PaginateDto<ExtraCurricularDto> AllExtraCurriculars { get; set; }
        public PaginateDto<UserDto> AllExTeacher { get; set; }
        public PaginateDto<AcadmicYearDto> AllAcadmicYears { get; set; }
        public PaginateDto<TermDto> AllTerms { get; set; }
        public PaginateDto<UserDto> AllHeadOfEducations { get; set; }
        public PaginateDto<DepartmentDto> AllDepartments { get; set; }
    }
}
    

