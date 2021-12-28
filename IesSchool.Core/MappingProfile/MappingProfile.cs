using AutoMapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.InfraStructure.Paging;
using static IesSchool.Core.Dto.MembershipDto;

namespace IesSchool.Core.MappingProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<State, StateDto>().ReverseMap();
            CreateMap<City, CityDto>().ForMember(x => x.State, op => op.Ignore()).ReverseMap().ForMember(x => x.State, op => op.Ignore());
            CreateMap(typeof(Paginate<>), typeof(PaginateDto<>)).ReverseMap();
            CreateMap(typeof(List<>), typeof(List<>)).ReverseMap();
            CreateMap<ExtraCurricular, ExtraCurricularDto>().ReverseMap();
            CreateMap<ParamedicalService, ParamedicalServiceDto>().ReverseMap();

            CreateMap<Department, DepartmentDto>()
            .ForMember(dist => dist.SkillsCount, opt => opt.MapFrom(c => c.SkillAlowedDepartments.Count()))
            .ForMember(dist => dist.StudentCount, opt => opt.MapFrom(c => c.Students.Count()))
            .ForMember(dist => dist.TeacherCount, opt => opt.MapFrom(c => c.Users.Where(x => x.IsTeacher == true).Count()))
            .ReverseMap();

            CreateMap<AcadmicYear, AcadmicYearDto>().ReverseMap();
            CreateMap<Term, TermDto>().ReverseMap();


            CreateMap<AttachmentType, AttachmentTypeDto>().ReverseMap();
            CreateMap<WorkCategory, WorkCategoryDto>().ReverseMap(); 

            CreateMap<Area, AreaDto>()
            .ForMember(dist => dist.StrandsCount, opt => opt.MapFrom(c => c.Strands.Count()))
            .ForMember(dist => dist.SkillsCount, opt => opt.MapFrom(c => c.Strands.ToList().Where(s => s.Skills.Count > 0).Select(s => s.Skills).Sum(d => d.Count))).ReverseMap()
            .ForMember(x => x.Strands, op => op.Ignore());
            CreateMap<Area, AreaDetailsDto>().ReverseMap();

            CreateMap<Strand, StrandDto>()
           .ForMember(dist => dist.SkillsCount, opt => opt.MapFrom(c => c.Skills.Count()))
            .ForMember(dist => dist.AreaName, opt => opt.MapFrom(c => c.Area == null ? "" : c.Area.Name))
            .ReverseMap()
            .ForMember(x => x.Skills, op => op.Ignore())
            .ForMember(x => x.Area, op => op.Ignore()).ReverseMap();

            CreateMap<Strand, StrandSkillsDto>()
           .ForMember(dist => dist.SkillsCount, opt => opt.MapFrom(c => c.Skills.Count()))
            .ForMember(dist => dist.AreaName, opt => opt.MapFrom(c => c.Area == null ? "" : c.Area.Name))
           .ReverseMap();

            CreateMap<Skill, SkillDto>()
            .ForMember(dist => dist.AreaName, opt => opt.MapFrom(c => c.Strand == null ? "" : c.Strand.Area.Name))
            .ForMember(dist => dist.StrandName, opt => opt.MapFrom(c => c.Strand == null ? "" : c.Strand.Name))
            .ForMember(dist => dist.AllowedDepartments, opt => opt.MapFrom(c => c.SkillAlowedDepartments.ToList().Select(x => x.Department.Name).Distinct())).ReverseMap()
            .ForMember(x => x.Strand, op => op.Ignore())
            .ForMember(x => x.ObjectiveSkills, op => op.Ignore()).ReverseMap();

            CreateMap<SkillEvaluation, SkillEvaluationDto>().ReverseMap();
            CreateMap<SkillAlowedDepartment, SkillAlowedDepartmentDto>().ReverseMap();
            CreateMap<VwSkill, VwSkillDto>().ReverseMap();


            CreateMap<Assistant, AssistantDto>()
            .ReverseMap()
            .ForMember(x => x.Department, op => op.Ignore())
            .ForMember(x => x.Nationality, op => op.Ignore());
            CreateMap<VwAssistant, VwAssistantDto>().ReverseMap();
            CreateMap<AssistantHelper, AssistantHelperDto>().ReverseMap();

           

            CreateMap<Student, StudentDto>().ReverseMap()
            .ForMember(x => x.Department, op => op.Ignore())
            .ForMember(x => x.Nationality, op => op.Ignore());
            CreateMap<StudentTherapist, StudentTherapistDto>()
             .ForMember(dist => dist.StudentName, opt => opt.MapFrom(c => c.Student == null ? "" : c.Student.Name))
             .ForMember(dist => dist.StudentNameAr, opt => opt.MapFrom(c => c.Student == null ? "" : c.Student.NameAr))
             .ForMember(dist => dist.DepartmentId, opt => opt.MapFrom(c => c.Student == null ? 0 : c.Student.DepartmentId))
             .ForMember(dist => dist.StudentCode, opt => opt.MapFrom(c => c.Student == null ? 0 : c.Student.Code)).ReverseMap();
            CreateMap<StudentHistoricalSkill, StudentHistoricalSkillDto>().ReverseMap();
            //CreateMap<List<StudentHistoricalSkill>,List< StudentHistoricalSkillDto>>().ReverseMap();
            CreateMap<StudentAttachment, StudentAttachmentDto>().ReverseMap();
            CreateMap<StudentHelper, StudentHelperDto>().ReverseMap();
            CreateMap<UserAttachment, UserAttachmentDto>().ReverseMap();
            CreateMap<VwStudent, VwStudentDto>().ReverseMap();
            CreateMap<Student, StudentDetailsDto>().ReverseMap();
            CreateMap<StudentAttachmentBinary, StudentAttachmentBinaryDto>().ReverseMap();
            CreateMap<Phone, PhoneDto>().ReverseMap();
            CreateMap<Religion, ReligionDto>().ReverseMap();
            CreateMap<Paginate<StudentHistoricalSkill>, Paginate<StudentHistoricalSkillDto>>().ReverseMap();
            CreateMap<Paginate<Phone>, Paginate<PhoneDto>>().ReverseMap();
            CreateMap<Paginate<StudentAttachment>, Paginate<StudentAttachmentDto>>().ReverseMap();



            CreateMap<User, UserDto>().ReverseMap()
            .ForMember(x => x.Department, op => op.Ignore())
            .ForMember(x => x.Nationality, op => op.Ignore());
            CreateMap<AspNetUser, AspNetUserDto>().ReverseMap();
            CreateMap<VwUser, VwUserDto>().ReverseMap();
            CreateMap<UserHelper, UserHelperDto>().ReverseMap();
            CreateMap<UserAssistant, UserAssistantDto>().ReverseMap()
           .ForMember(x => x.Assistant, op => op.Ignore()).ForMember(x => x.User, op => op.Ignore());
            CreateMap<TherapistParamedicalService, TherapistParamedicalServiceDto>()
             .ForMember(dist => dist.ParamedicalServiceName, opt => opt.MapFrom(c => c.ParamedicalService == null ? "" : c.ParamedicalService.Name))
             .ForMember(dist => dist.ParamedicalServiceNameAr, opt => opt.MapFrom(c => c.ParamedicalService == null ? "" : c.ParamedicalService.NameAr))
             .ReverseMap();



            CreateMap<Iep, IepDto>().ReverseMap();
            CreateMap<Iep, GetIepDto>()
            .ForMember(dist => dist.StudentName, opt => opt.MapFrom(c => c.Student == null ? "" : c.Student.Name))
            .ForMember(dist => dist.StudentCodeName, opt => opt.MapFrom(c => c.Student == null ? "" : c.Student.Code.ToString()))
            .ForMember(dist => dist.StudentBirthdayName, opt => opt.MapFrom(c => c.Student == null ? "" : c.Student.DateOfBirth.Value.ToShortDateString()))
            .ForMember(dist => dist.DepartmentName, opt => opt.MapFrom(c => c.Student == null ? "" : c.Student.Department== null ? "" : c.Student.Department.Name))
            .ForMember(dist => dist.AcadmicYearName, opt => opt.MapFrom(c => c.AcadmicYear == null ? "" : c.AcadmicYear.Name))
            .ForMember(dist => dist.TermName, opt => opt.MapFrom(c => c.Term == null ? "" : c.Term.Name))
            .ForMember(dist => dist.TeacherName, opt => opt.MapFrom(c => c.Teacher == null ? "" : c.Teacher.Name))
            .ForMember(dist => dist.HeadOfDepartmentName, opt => opt.MapFrom(c => c.HeadOfDepartmentNavigation == null ? "" : c.HeadOfDepartmentNavigation.Name))
            .ForMember(dist => dist.HeadOfEducationName, opt => opt.MapFrom(c => c.HeadOfEducationNavigation == null ? "" : c.HeadOfEducationNavigation.Name))
            .ReverseMap();

            CreateMap<IepHelper, IepHelperDto>().ReverseMap();
            CreateMap<VwIep, VwIepDto>().ReverseMap();
            CreateMap<IepAssistant, IepAssistantDto>()
            .ForMember(dist => dist.AssistantName, opt => opt.MapFrom(c => c.Assistant == null ? "" : c.Assistant.Name))
            .ReverseMap();
            CreateMap<IepExtraCurricular, IepExtraCurricularDto>()
            .ForMember(dist => dist.ExtraCurricularName, opt => opt.MapFrom(c => c.ExtraCurricular == null ? "" : c.ExtraCurricular.Name))
            .ReverseMap();
            CreateMap<IepParamedicalService, IepParamedicalServiceDto>()
            .ForMember(dist => dist.ParamedicalServiceName, opt => opt.MapFrom(c => c.ParamedicalService == null ? "" : c.ParamedicalService.Name))
            .ReverseMap();
            CreateMap<Paginate<Objective>, Paginate<ObjectiveDto>>().ReverseMap();


            CreateMap<Goal, GoalDto>()
             .ForMember(dist => dist.AreaName, opt => opt.MapFrom(c => c.Area == null ? "" : c.Area.Name))
             .ForMember(dist => dist.StrandName, opt => opt.MapFrom(c => c.Strand == null ? "" : c.Strand.Name))
             .ReverseMap()
             .ForMember(x => x.Area, op => op.Ignore())
             .ForMember(x => x.Strand, op => op.Ignore())
             .ForMember(x => x.Skill, op => op.Ignore());
             //.ForMember(x => x.Objectives, op => op.Ignore());

            CreateMap<Goal, GetGoalDto>()
            .ForMember(dist => dist.AreaName, opt => opt.MapFrom(c => c.Area == null ? "" : c.Area.Name))
            .ForMember(dist => dist.StrandName, opt => opt.MapFrom(c => c.Strand == null ? "" : c.Strand.Name))
            .ReverseMap()
            .ForMember(x => x.Area, op => op.Ignore())
            .ForMember(x => x.Strand, op => op.Ignore())
            .ForMember(x => x.Skill, op => op.Ignore())
            .ForMember(x => x.Objectives, op => op.Ignore());


            CreateMap<Objective, ObjectiveDto>().ReverseMap();
            //.ForMember(x => x.ObjectiveEvaluationProcesses, op => op.Ignore())
            //.ForMember(x => x.ObjectiveSkills, op => op.Ignore())
            //.ForMember(x => x.Activities, op => op.Ignore());

            CreateMap<Objective, GetObjectiveDto>()
           .ForMember(dist => dist.EvaluationProcessName, opt => opt.MapFrom(c => c.ObjectiveEvaluationProcesses.ToList().Select(x => x.SkillEvaluation.Name).Distinct()))
           .ForMember(dist => dist.ObjSkillsIds, opt => opt.MapFrom(c => c.ObjectiveSkills.ToList().Select(x => x.SkillId).Distinct())).ReverseMap()
           .ForMember(x => x.Goal, op => op.Ignore());

              
            CreateMap<ObjectiveEvaluationProcess, ObjectiveEvaluationProcessDto>().ReverseMap();
            CreateMap<ObjectiveSkill, ObjectiveSkillDto>().ReverseMap();

            CreateMap<Activity, ActivityDto>()
             .ForMember(dist => dist.ObjectiveNote, opt => opt.MapFrom(c => c.Objective == null ? "" : c.Objective.ObjectiveNote))
             .ReverseMap()
             .ForMember(x => x.Objective, op => op.Ignore());

            CreateMap<Event, EventDto>().ReverseMap()
            .ForMember(x => x.Department, op => op.Ignore());
            CreateMap<Event, EventGetDto>().ReverseMap();

            CreateMap<EventAttachement, EventAttachementDto>().ReverseMap()
             .ForMember(x => x.Event, op => op.Ignore());

            CreateMap<EventTeacher, EventTeacherDto>()
            .ForMember(dist => dist.TeacherName, opt => opt.MapFrom(c => c.Teacher == null ? "" : c.Teacher.Name))
                .ReverseMap()
             .ForMember(x => x.Event, op => op.Ignore());

            CreateMap<EventStudent, EventStudentDto>()
            .ForMember(dist => dist.EventName, opt => opt.MapFrom(c => c.Event == null ? "" : c.Event.Name))
            .ForMember(dist => dist.StudentName, opt => opt.MapFrom(c => c.Student == null ? "" : c.Student.Name))
            .ForMember(dist => dist.StudentNameAr, opt => opt.MapFrom(c => c.Student == null ? "" : c.Student.NameAr))
                .ReverseMap()
             .ForMember(x => x.Event, op => op.Ignore())
             .ForMember(x => x.EventStudentFiles, op => op.Ignore()).ReverseMap();

            CreateMap< List<EventStudent>, List<EventStudentDto>>().ReverseMap();

            CreateMap<EventHelper, EventHelperDto>().ReverseMap();
            CreateMap<EventType, EventTypeDto>().ReverseMap();
            CreateMap<EventStudentFile, EventStudentFileDto>().ReverseMap();
            CreateMap<Setting, SettingDto>().ReverseMap();




            CreateMap<ApplicationGroup, ApplicationGroupDto>().ReverseMap();
            CreateMap<ApplicationGroupRole, ApplicationGroupRoleDto>().ReverseMap();
            CreateMap<ApplicationUserGroup, ApplicationUserGroupDto>().ReverseMap();

            CreateMap<ReportsHelper, ReportsHelperDto>().ReverseMap();
            CreateMap<VwIep, IepLPReportDto>().ReverseMap();
            CreateMap<VwIep, IepReportDto>().ReverseMap();

        }
    }
}
