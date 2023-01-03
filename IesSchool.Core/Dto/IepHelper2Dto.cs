using IesSchool.Context.Models;
using IesSchool.InfraStructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    internal class IepHelper2
    {

        public PaginateDto<Program> AllPrograms { get; set; }
        public PaginateDto<Area> AllAreas { get; set; }
        public PaginateDto<Strand> AllStrands { get; set; }
        public PaginateDto<ParamedicalService> AllParamedicalServices { get; set; }
        public PaginateDto<ExtraCurricular> AllExtraCurriculars { get; set; }
        public PaginateDto<User> AllExtraCurricularsTeachers { get; set; }
        public PaginateDto<SkillEvaluation> AllSkillEvaluations { get; set; }
        public PaginateDto<User> AllTherapist { get; set; }
        public PaginateDto<TherapistParamedicalService> TherapistParamedicalService { get; set; }
        public PaginateDto<UserExtraCurricular> UserExtraCurricular { get; set; }
        public PaginateDto<StudentTherapist> AllStudentTherapist { get; set; }
        public PaginateDto<StudentExtraTeacher> AllStudentExtraTeacher { get; set; }
        public Setting Setting { get; set; }
    }
    public class IepHelper2Dto
    {
        public IPaginate<Program> AllPrograms { get; set; }
        public PaginateDto<AreaDto> AllAreas { get; set; }
        public PaginateDto<StrandDto> AllStrands { get; set; }
        public PaginateDto<ParamedicalServiceDto> AllParamedicalServices { get; set; }
        public PaginateDto<ExtraCurricularDto> AllExtraCurriculars { get; set; }
        public PaginateDto<UserDto> AllExtraCurricularsTeachers { get; set; }
        public PaginateDto<SkillEvaluationDto> AllSkillEvaluations { get; set; }
        public PaginateDto<UserDto> AllTherapist { get; set; }
        public PaginateDto<TherapistParamedicalServiceDto> TherapistParamedicalService { get; set; }
        public PaginateDto<UserExtraCurricularDto> UserExtraCurricular { get; set; }
        public PaginateDto<StudentTherapistDto> AllStudentTherapist { get; set; }
        public PaginateDto<StudentExtraTeacherDto> AllStudentExtraTeacher { get; set; }
        public SettingDto Setting { get; set; }
    }
}
