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

        public IPaginate<Area> AllAreas { get; set; }
        public IPaginate<Strand> AllStrands { get; set; }
        public IPaginate<ParamedicalService> AllParamedicalServices { get; set; }
        public IPaginate<ExtraCurricular> AllExtraCurriculars { get; set; }
        public IPaginate<User> AllExtraCurricularsTeachers { get; set; }
        public IPaginate<SkillEvaluation> AllSkillEvaluations { get; set; }
        public IPaginate<User> AllTherapist { get; set; }
        public IPaginate<TherapistParamedicalService> TherapistParamedicalService { get; set; }
        public IPaginate<UserExtraCurricular> UserExtraCurricular { get; set; }
        public IPaginate<StudentTherapist> AllStudentTherapist { get; set; }
        public IPaginate<StudentExtraTeacher> AllStudentExtraTeacher { get; set; }
    }
    public class IepHelper2Dto
    {

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

    }
}
