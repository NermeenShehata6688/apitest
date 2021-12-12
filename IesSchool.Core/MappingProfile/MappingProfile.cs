using AutoMapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.InfraStructure.Paging;

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
           .ForMember(dist => dist.SkillsCount, opt => opt.MapFrom(c => c.Skills.Count())).ReverseMap()
            .ForMember(x => x.Skills, op => op.Ignore())
            .ForMember(x => x.Area, op => op.Ignore()).ReverseMap();

            CreateMap<Strand, StrandSkillsDto>()
           .ForMember(dist => dist.SkillsCount, opt => opt.MapFrom(c => c.Skills.Count())).ReverseMap()
           .ReverseMap();

            CreateMap<Skill, SkillDto>()
            .ForMember(dist => dist.AreaName, opt => opt.MapFrom(c => c.Strand == null ? "" : c.Strand.Area.Name))
            .ForMember(dist => dist.StrandName, opt => opt.MapFrom(c => c.Strand == null ? "" : c.Strand.Name))
            .ForMember(dist => dist.AllowedDepartments, opt => opt.MapFrom(c => c.SkillAlowedDepartments.ToList().Select(x => x.Department.Name).Distinct())).ReverseMap()
            .ForMember(x => x.Strand, op => op.Ignore()).ReverseMap();
            CreateMap<SkillEvaluation, SkillEvaluationDto>().ReverseMap();
            CreateMap<SkillAlowedDepartment, SkillAlowedDepartmentDto>().ReverseMap();


            CreateMap<Assistant, AssistantDto>()
                .ReverseMap()
            .ForMember(x => x.Department, op => op.Ignore())
            .ForMember(x => x.Nationality, op => op.Ignore());
            CreateMap<VwAssistant, VwAssistantDto>().ReverseMap();
            CreateMap<AssistantHelper, AssistantHelperDto>().ReverseMap();

           

            CreateMap<Student, StudentDto>().ReverseMap()
            .ForMember(x => x.Department, op => op.Ignore())
            .ForMember(x => x.Nationality, op => op.Ignore());
            CreateMap<StudentTherapist, StudentTherapistDto>().ReverseMap();
            CreateMap<StudentHistoricalSkill, StudentHistoricalSkillDto>().ReverseMap();
            //CreateMap<List<StudentHistoricalSkill>,List< StudentHistoricalSkillDto>>().ReverseMap();
            CreateMap<StudentAttachment, StudentAttachmentDto>().ReverseMap();
            CreateMap<StudentHelper, StudentHelperDto>().ReverseMap();
            CreateMap<UserAttachment, UserAttachmentDto>().ReverseMap();
            CreateMap<VwStudent, VwStudentDto>().ReverseMap();
            CreateMap<Student, StudentDetailsDto>().ReverseMap();
            CreateMap<StudentAttachmentBinary, StudentAttachmentBinaryDto>().ReverseMap();
            CreateMap<Phone, PhoneDto>().ReverseMap();


            CreateMap<User, UserDto>().ReverseMap()
            .ForMember(x => x.Department, op => op.Ignore())
            .ForMember(x => x.Nationality, op => op.Ignore());
            CreateMap<AspNetUser, AspNetUserDto>().ReverseMap();
            CreateMap<VwUser, VwUserDto>().ReverseMap();
            CreateMap<UserHelper, UserHelperDto>().ReverseMap();
            CreateMap<UserAssistant, UserAssistantDto>().ReverseMap()
           .ForMember(x => x.Assistant, op => op.Ignore()).ForMember(x => x.User, op => op.Ignore());
            CreateMap<TherapistParamedicalService, TherapistParamedicalServiceDto>().ReverseMap();



            CreateMap<Iep, IepDto>().ReverseMap();
            CreateMap<IepHelper, IepHelperDto>().ReverseMap();
            CreateMap<IepAssistant, IepAssistantDto>().ReverseMap();
            CreateMap<IepExtraCurricular, IepExtraCurricularDto>().ReverseMap();
            CreateMap<IepParamedicalService, IepParamedicalServiceDto>().ReverseMap();

            CreateMap<Goal, GoalDto>()
             .ForMember(dist => dist.AreaName, opt => opt.MapFrom(c => c.Area == null ? "" : c.Area.Name))
             .ForMember(dist => dist.StrandName, opt => opt.MapFrom(c => c.Strand == null ? "" : c.Strand.Name)).ReverseMap()
             .ForMember(x => x.Area, op => op.Ignore())
             .ForMember(x => x.Strand, op => op.Ignore())
             .ForMember(x => x.Skill, op => op.Ignore());

            CreateMap<Objective, ObjectiveDto>().ReverseMap();
            CreateMap<ObjectiveEvaluationProcess, ObjectiveEvaluationProcessDto>().ReverseMap();
            CreateMap<ObjectiveSkill, ObjectiveSkillDto>().ReverseMap();

            CreateMap<Setting, SettingDto>().ReverseMap();

        }
    }
}
