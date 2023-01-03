using IesSchool.Context.Models;
using IesSchool.InfraStructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class IepHelper
    {
        public PaginateDto<VwStudent> AllStudents { get; set; }
        public PaginateDto<AcadmicYear> AllAcadmicYears { get; set; }
        public PaginateDto<Term> AllTerms { get; set; }
        public PaginateDto<User> AllTeachers { get; set; }
        public PaginateDto<Assistant> AllAssistants { get; set; }
        public PaginateDto<User> AllHeadOfEducations { get; set; }
        public PaginateDto<Department> AllDepartments { get; set; }
       
        public PaginateDto<UserAssistant> AllTeacherAssistants { get; set; }
        public Setting Setting { get; set; }


    }
    public class IepHelperDto
    {
        public PaginateDto<VwStudentDto> AllStudents { get; set; }
        public PaginateDto<AcadmicYearDto> AllAcadmicYears { get; set; }
        public PaginateDto<TermDto> AllTerms { get; set; }
        public PaginateDto<UserDto> AllTeachers { get; set; }
        public PaginateDto<AssistantDto> AllAssistants { get; set; }
        public PaginateDto<UserDto> AllHeadOfEducations { get; set; }
        public PaginateDto<DepartmentDto> AllDepartments { get; set; }
        public PaginateDto<UserAssistantDto> AllTeacherAssistants { get; set; }
        public SettingDto Setting { get; set; }


    }
}
