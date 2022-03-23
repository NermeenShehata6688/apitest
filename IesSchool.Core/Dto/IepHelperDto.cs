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
        public IPaginate<VwStudent> AllStudents { get; set; }
        public IPaginate<AcadmicYear> AllAcadmicYears { get; set; }
        public IPaginate<Term> AllTerms { get; set; }
        public IPaginate<User> AllTeachers { get; set; }
        public IPaginate<Assistant> AllAssistants { get; set; }
        public IPaginate<User> AllHeadOfEducations { get; set; }
        public IPaginate<Department> AllDepartments { get; set; }
       
        public IPaginate<UserAssistant> AllTeacherAssistants { get; set; }
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
