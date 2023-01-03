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
        public PaginateDto<VwStudent> AllStudents { get; set; }
        public PaginateDto<ExtraCurricular> AllExtraCurriculars { get; set; }
        public PaginateDto<User> AllExTeacher { get; set; }
        public PaginateDto<AcadmicYear> AllAcadmicYears { get; set; }
        public PaginateDto<Term> AllTerms { get; set; }
        public PaginateDto<User> AllHeadOfEducations { get; set; }
        public PaginateDto<Department> AllDepartments { get; set; }
        public PaginateDto<UserExtraCurricular> UserExtraCurricular { get; set; }
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
        public PaginateDto<UserExtraCurricularDto> UserExtraCurricular { get; set; }
    }
}
    

