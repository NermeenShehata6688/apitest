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
        public IPaginate<Area> AllAreas { get; set; }
        public IPaginate<Strand> AllStrands { get; set; }
        public IPaginate<Skill> AllSkills { get; set; }
        public IPaginate<ParamedicalService> AllParamedicalServices { get; set; }
        public IPaginate<ExtraCurricular> AllExtraCurriculars { get; set; }
        public IPaginate<Department> AllDepartments { get; set; }
        public IPaginate<SkillEvaluation> AllSkillEvaluations { get; set; }
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
        public PaginateDto<AreaDto> AllAreas { get; set; }
        public PaginateDto<StrandDto> AllStrands { get; set; }
        public PaginateDto<SkillDto> AllSkills { get; set; }
        public PaginateDto<ParamedicalServiceDto> AllParamedicalServices { get; set; }
        public PaginateDto<ExtraCurricularDto> AllExtraCurriculars { get; set; }
        public PaginateDto<DepartmentDto> AllDepartments { get; set; }
        public PaginateDto<SkillEvaluationDto> AllSkillEvaluations { get; set; }
        public SettingDto Setting { get; set; }


    }
}
