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
            #region BasicData
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<State, StateDto>().ReverseMap();
            CreateMap<City, CityDto>().ForMember(x => x.State, op => op.Ignore()).ReverseMap().ForMember(x => x.State, op => op.Ignore());

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

            #endregion
            #region Curriculums
            CreateMap<Area, AreaDto>()
          .ForMember(dist => dist.StrandsCount, opt => opt.MapFrom(c => c.Strands.Count()))
          .ForMember(dist => dist.SkillsCount, opt => opt.MapFrom(c => c.Strands.ToList().Where(s => s.Skills.Count > 0).Select(s => s.Skills).Sum(d => d.Count))).ReverseMap()
          .ForMember(x => x.Strands, op => op.Ignore());
            CreateMap<Area, AreaDetailsDto>().ReverseMap();

            CreateMap<Strand, StrandDto>()
           .ForMember(dist => dist.SkillsCount, opt => opt.MapFrom(c => c.Skills.Count()))
            .ForMember(dist => dist.AreaName, opt => opt.MapFrom(c => c.Area == null ? "" : c.Area.Name))
            .ForMember(dist => dist.MaxSkillForStrand, opt => opt.MapFrom(c => c.Skills == null ? 0 : c.Skills.ToList().Where(x => x.StrandId == c.Id) == null ? 0 : c.Skills.Where(x => x.StrandId == c.Id).Select(x => x.SkillNumber).ToList().Max()))
            //.ForMember(dist => dist.LastIndexForStrand, opt => opt.MapFrom(c => c.Skills == null ? 0 : c.Skills.Where(x=> x.StrandId==c.Id)==null?0: c.Skills.Where(x => x.StrandId == c.Id).Select(x=> x.SkillNumber).ToList().Last())
            .ReverseMap()
            .ForMember(x => x.Skills, op => op.Ignore())
            .ForMember(x => x.Area, op => op.Ignore()).ReverseMap();

            CreateMap<Strand, StrandSkillsDto>()
           .ForMember(dist => dist.SkillsCount, opt => opt.MapFrom(c => c.Skills.Count()))
            .ForMember(dist => dist.AreaName, opt => opt.MapFrom(c => c.Area == null ? "" : c.Area.Name))
           .ReverseMap();

            CreateMap<Skill, SkillDto>()
            .ForMember(dist => dist.AreaName, opt => opt.MapFrom(c => c.Strand == null ? "" : c.Strand.Area == null ? "" : c.Strand.Area.Name))
            .ForMember(dist => dist.StrandName, opt => opt.MapFrom(c => c.Strand == null ? "" : c.Strand.Name))
            .ForMember(dist => dist.AllowedDepartments, opt => opt.MapFrom(c => c.SkillAlowedDepartments.ToList().Select(x => x.Department == null ? "" : x.Department.Name).Distinct())).ReverseMap()
            .ForMember(x => x.Strand, op => op.Ignore())
            .ForMember(x => x.ObjectiveSkills, op => op.Ignore()).ReverseMap();

            CreateMap<SkillEvaluation, SkillEvaluationDto>().ReverseMap();
            CreateMap<SkillAlowedDepartment, SkillAlowedDepartmentDto>().ReverseMap();
            CreateMap<VwSkill, VwSkillDto>().ReverseMap();
            #endregion
            #region Student
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
            CreateMap<VwStudent, VwStudentDto>().ReverseMap();
            CreateMap<Student, StudentDetailsDto>().ReverseMap();
            CreateMap<StudentAttachmentBinary, StudentAttachmentBinaryDto>().ReverseMap();
            CreateMap<Phone, PhoneDto>().ReverseMap();
            CreateMap<Religion, ReligionDto>().ReverseMap();
            CreateMap<Paginate<StudentHistoricalSkill>, Paginate<StudentHistoricalSkillDto>>().ReverseMap();
            CreateMap<Paginate<Phone>, Paginate<PhoneDto>>().ReverseMap();
            CreateMap<StudentAttachment, StudentAttachmentDto>()
            .ForMember(dist => dist.AttachmentTypeName, opt => opt.MapFrom(c => c.AttachmentType == null ? "" : c.AttachmentType.Name))
            .ReverseMap()
            .ForMember(x => x.Student, op => op.Ignore())
            .ForMember(x => x.AttachmentType, op => op.Ignore());
               
            #endregion
            #region User&Assistant
            CreateMap<Assistant, AssistantDto>()
          .ReverseMap()
          .ForMember(x => x.Department, op => op.Ignore())
          .ForMember(x => x.Nationality, op => op.Ignore());
            CreateMap<VwAssistant, VwAssistantDto>().ReverseMap();
            CreateMap<AssistantHelper, AssistantHelperDto>().ReverseMap();


            CreateMap<UserAttachment, UserAttachmentDto>()
             .ForMember(dist => dist.AttachmentTypeName, opt => opt.MapFrom(c => c.AttachmentType == null ? "" : c.AttachmentType.Name))
                .ReverseMap()
                .ForMember(x => x.User, op => op.Ignore())
            .ForMember(x => x.AttachmentType, op => op.Ignore());

            CreateMap<User, UserDto>().ReverseMap()
            .ForMember(x => x.Department, op => op.Ignore())
            .ForMember(x => x.Nationality, op => op.Ignore());
            CreateMap<AspNetUser, AspNetUserDto>().ReverseMap();
            CreateMap<VwUser, VwUserDto>().ReverseMap();
            CreateMap<UserHelper, UserHelperDto>().ReverseMap();
            CreateMap<UserAssistant, UserAssistantDto>()
             .ForMember(dist => dist.AssistantName, opt => opt.MapFrom(c => c.Assistant == null ? "" : c.Assistant.Name))
                .ReverseMap()
           .ForMember(x => x.Assistant, op => op.Ignore()).ForMember(x => x.User, op => op.Ignore());
            CreateMap<TherapistParamedicalService, TherapistParamedicalServiceDto>()
             .ForMember(dist => dist.ParamedicalServiceName, opt => opt.MapFrom(c => c.ParamedicalService == null ? "" : c.ParamedicalService.Name))
             .ForMember(dist => dist.ParamedicalServiceNameAr, opt => opt.MapFrom(c => c.ParamedicalService == null ? "" : c.ParamedicalService.NameAr))
             .ReverseMap();

            #endregion
            #region IEP
            CreateMap<Iep, IepDto>().ReverseMap()
           .ForMember(x => x.Student, op => op.Ignore())
             .ForMember(x => x.Teacher, op => op.Ignore())
             .ForMember(x => x.AcadmicYear, op => op.Ignore())
             .ForMember(x => x.Term, op => op.Ignore())
             .ForMember(x => x.HeadOfDepartment, op => op.Ignore())
             .ForMember(x => x.HeadOfEducation, op => op.Ignore())
           //.ForMember(x => x.IepExtraCurriculars, op => op.Ignore())
           //.ForMember(x => x.IepParamedicalServices, op => op.Ignore())
           .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Iep, GetIepDto>()
            .ForMember(dist => dist.StudentName, opt => opt.MapFrom(c => c.Student == null ? "" : c.Student.Name))
            .ForMember(dist => dist.StudentCodeName, opt => opt.MapFrom(c => c.Student == null ? "" : c.Student.Code.ToString()))
            .ForMember(dist => dist.StudentBirthdayName, opt => opt.MapFrom(c => c.Student == null ? "" : c.Student.DateOfBirth == null ? "" : c.Student.DateOfBirth.Value.ToShortDateString()))
            .ForMember(dist => dist.DepartmentName, opt => opt.MapFrom(c => c.Student == null ? "" : c.Student.Department == null ? "" : c.Student.Department.Name))
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
            .ReverseMap()
             .ForMember(x => x.Assistant, op => op.Ignore());

            CreateMap<IepExtraCurricular, IepExtraCurricularDto>()
            .ForMember(dist => dist.ExtraCurricularName, opt => opt.MapFrom(c => c.ExtraCurricular == null ? "" : c.ExtraCurricular.Name))
            .ReverseMap()
             .ForMember(x => x.ExtraCurricular, op => op.Ignore());

            CreateMap<IepProgressReport, IepProgressReportDto>()

            .ForMember(dist => dist.TeacherName, opt => opt.MapFrom(c => c.Teacher == null ? "" : c.Teacher.Name))
            .ForMember(dist => dist.StudentDepartmentName, opt => opt.MapFrom(c => c.Student == null ? "" : c.Student.Department == null ? "" : c.Student.Department.Name))
            .ForMember(dist => dist.StudentBirthDay, opt => opt.MapFrom(c => c.Student == null ? "" : c.Student.DateOfBirth.ToString()))
            .ForMember(dist => dist.StudentCode, opt => opt.MapFrom(c => c.Student == null ? "" : c.Student.Code.ToString()))
            .ForMember(dist => dist.StudentName, opt => opt.MapFrom(c => c.Student == null ? "" : c.Student.Name))
            .ForMember(dist => dist.AcadmicYearName, opt => opt.MapFrom(c => c.AcadmicYear == null ? "" : c.AcadmicYear.Name))
            .ForMember(dist => dist.TermName, opt => opt.MapFrom(c => c.Term == null ? "" : c.Term.Name))
            .ForMember(dist => dist.StrandsCount, opt => opt.MapFrom(c => c.ProgressReportStrands.Count()))
            .ForMember(dist => dist.ParamedicalCount, opt => opt.MapFrom(c => c.ProgressReportParamedicals.Count()))
            .ForMember(dist => dist.ExtraCount, opt => opt.MapFrom(c => c.ProgressReportExtraCurriculars.Count()))
                .ReverseMap()
             .ForMember(x => x.Student, op => op.Ignore())
             .ForMember(x => x.AcadmicYear, op => op.Ignore())
             .ForMember(x => x.Iep, op => op.Ignore())
             .ForMember(x => x.Teacher, op => op.Ignore())
             .ForMember(x => x.Term, op => op.Ignore());
            //.ForMember(dest => dest.ProgressReportExtraCurriculars, o =>
            //{
            //    o.Condition(src => src.ProgressReportExtraCurriculars.Count == 0);
            //    o.Ignore();
            //});
            //.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ProgressReportExtraCurricular, ProgressReportExtraCurricularDto>()
           .ForMember(dist => dist.ExtraCurricularName, opt => opt.MapFrom(c => c.ExtraCurricular == null ? "" : c.ExtraCurricular.Name))
           .ForMember(dist => dist.ExtraCurricularNameAr, opt => opt.MapFrom(c => c.ExtraCurricular == null ? "" : c.ExtraCurricular.NameAr))
               .ReverseMap()
            .ForMember(x => x.ExtraCurricular, op => op.Ignore());

            CreateMap<ProgressReportParamedical, ProgressReportParamedicalDto>()
            .ForMember(dist => dist.ParamedicalServiceName, opt => opt.MapFrom(c => c.ParamedicalService == null ? "" : c.ParamedicalService.Name))
            .ForMember(dist => dist.ParamedicalServiceNameAr, opt => opt.MapFrom(c => c.ParamedicalService == null ? "" : c.ParamedicalService.NameAr))
                .ReverseMap()
             .ForMember(x => x.ParamedicalService, op => op.Ignore());

            CreateMap<ProgressReportStrand, ProgressReportStrandDto>()
            .ForMember(dist => dist.StrandName, opt => opt.MapFrom(c => c.Strand == null ? "" : c.Strand.Name))
            .ForMember(dist => dist.AreaName, opt => opt.MapFrom(c => c.Strand == null ? "" : c.Strand.Area == null ? "" : c.Strand.Area.Name))
                .ReverseMap()
             .ForMember(x => x.Strand, op => op.Ignore());


            CreateMap<IepParamedicalService, IepParamedicalServiceDto>()
            .ForMember(dist => dist.ParamedicalServiceName, opt => opt.MapFrom(c => c.ParamedicalService == null ? "" : c.ParamedicalService.Name))
            .ReverseMap()
             .ForMember(x => x.ParamedicalService, op => op.Ignore());
            CreateMap<Paginate<Objective>, Paginate<ObjectiveDto>>().ReverseMap();


            //CreateMap<Paginate<IepParamedicalService>, PaginateDto<IepParamedicalServiceDto>>()
            //    .ForMember(dist => dist.Items., opt => opt.MapFrom(c => c.ParamedicalService == null ? "" : c.ParamedicalService.Name))
            //.ReverseMap();
            CreateMap<Paginate<IepExtraCurricular>, Paginate<IepExtraCurricularDto>>().ReverseMap();



            CreateMap<Goal, GoalDto>()
             .ForMember(dist => dist.AreaName, opt => opt.MapFrom(c => c.Area == null ? "" : c.Area.Name))
             .ForMember(dist => dist.StrandName, opt => opt.MapFrom(c => c.Strand == null ? "" : c.Strand.Name))
             .ReverseMap()
             .ForMember(x => x.Area, op => op.Ignore())
             .ForMember(x => x.Strand, op => op.Ignore())
             .ForMember(x => x.Skill, op => op.Ignore());
            //.ForMember(x => x.Objectives, op => op.Ignore());
            CreateMap<Paginate<Goal>, Paginate<GoalDto>>().ReverseMap();


            CreateMap<Goal, GetGoalDto>()
            .ForMember(dist => dist.AreaName, opt => opt.MapFrom(c => c.Area == null ? "" : c.Area.Name))
            .ForMember(dist => dist.StrandName, opt => opt.MapFrom(c => c.Strand == null ? "" : c.Strand.Name))
            .ReverseMap()
            .ForMember(x => x.Area, op => op.Ignore())
            .ForMember(x => x.Strand, op => op.Ignore())
            .ForMember(x => x.Skill, op => op.Ignore())
            .ForMember(x => x.Objectives, op => op.Ignore());


            CreateMap<Objective, IepObjectiveDto>()
           .ForMember(dist => dist.AreaName, opt => opt.MapFrom(c => c.Goal == null ? "" : c.Goal.Area == null ? "" : c.Goal.Area.Name == null ? "" : c.Goal.Area.Name))
           .ForMember(dist => dist.StrandName, opt => opt.MapFrom(c => c.Goal == null ? "" : c.Goal.Strand == null ? "" : c.Goal.Strand.Name == null ? "" : c.Goal.Strand.Name))
            .ForMember(dist => dist.NoOfObjSkills, opt => opt.MapFrom(c => c.ObjectiveSkills == null ? 0 : c.ObjectiveSkills.Count()))
            .ForMember(dist => dist.NoOfObjActivities, opt => opt.MapFrom(c => c.Activities == null ? 0 : c.Activities.Count()))
           .ReverseMap();
            CreateMap<Objective, ObjectiveActivitiesDto>().ReverseMap();

            CreateMap<Objective, ObjectiveDto>()
           .ForMember(dist => dist.EvaluationProcessName, opt => opt.MapFrom(c => c.ObjectiveEvaluationProcesses.ToList().Select(x => x.SkillEvaluation == null ? "" : x.SkillEvaluation.Name).Distinct()))
           .ForMember(dist => dist.ObjSkillsNumbers, opt => opt.MapFrom(c => c.ObjectiveSkills.ToList().Select(c => c.Skill == null ? 0 : c.Skill.SkillNumber == null ? 0 : c.Skill.SkillNumber)))
           .ForMember(dist => dist.AreaName, opt => opt.MapFrom(c => c.Goal == null ? "" : c.Goal.Area == null ? "" : c.Goal.Area.Name))
           .ForMember(dist => dist.StrandName, opt => opt.MapFrom(c => c.Goal == null ? "" : c.Goal.Strand == null ? "" : c.Goal.Strand.Name))
           .ForMember(dist => dist.ObjActivitiesCount, opt => opt.MapFrom(c => c.Activities == null ? 0 : c.Activities.Count())).ReverseMap()
           .ForMember(x => x.Goal, op => op.Ignore()).ReverseMap();
            //.ForMember(x => x.ObjectiveEvaluationProcesses, op => op.Ignore())
            //.ForMember(x => x.ObjectiveSkills, op => op.Ignore())
            //.ForMember(x => x.Activities, op => op.Ignore());

            CreateMap<Objective, GetObjectiveDto>()
           .ForMember(dist => dist.EvaluationProcessName, opt => opt.MapFrom(c => c.ObjectiveEvaluationProcesses.ToList().Select(x => x.SkillEvaluation == null ? "" : x.SkillEvaluation.Name).Distinct()))
            .ForMember(dist => dist.ObjSkillsNumbers, opt => opt.MapFrom(c => c.ObjectiveSkills.ToList().Select(c => c.Skill == null ? 0 : c.Skill.SkillNumber == null ? 0 : c.Skill.SkillNumber))).ReverseMap()
           .ForMember(x => x.Goal, op => op.Ignore());


            CreateMap<ObjectiveEvaluationProcess, ObjectiveEvaluationProcessDto>().ReverseMap();

            CreateMap<ObjectiveSkill, ObjectiveSkillDto>()
            .ForMember(dist => dist.SkillNumber, opt => opt.MapFrom(c => c.Skill == null ? 0 : c.Skill.SkillNumber == null ? 0 : c.Skill.SkillNumber))
            .ReverseMap();

            CreateMap<Activity, ActivityDto>()
             .ForMember(dist => dist.ObjectiveNote, opt => opt.MapFrom(c => c.Objective == null ? "" : c.Objective.ObjectiveNote))
             .ReverseMap()
             .ForMember(x => x.Objective, op => op.Ignore());
            CreateMap<VwIep, IepLPReportDto>().ReverseMap();
            CreateMap<VwIep, IepReportDto>().ReverseMap();
            CreateMap<LogComment, LogCommentDto>().ReverseMap()
             .ForMember(x => x.Iep, op => op.Ignore())
             .ForMember(x => x.Student, op => op.Ignore())
             .ForMember(x => x.User, op => op.Ignore());
            #endregion
            #region Event
            CreateMap<Event, EventDto>()
           .ForMember(dist => dist.DepartmentName, opt => opt.MapFrom(c => c.Department == null ? "" : c.Department.Name == null ? "" : c.Department.Name))
           .ForMember(dist => dist.EventTypeName, opt => opt.MapFrom(c => c.EventType == null ? "" : c.EventType.Name == null ? "" : c.EventType.Name))
               .ReverseMap()
           .ForMember(x => x.Department, op => op.Ignore())
           .ForMember(x => x.EventType, op => op.Ignore());
            CreateMap<Event, EventGetDto>().ReverseMap();

            // CreateMap<Paginate<EventAttachement>, Paginate<EventAttachementDto>>().ReverseMap();

            CreateMap<EventAttachement, EventAttachementDto>().ReverseMap()
             .ForMember(x => x.Event, op => op.Ignore());

            CreateMap<EventAttachmentBinary, EventAttachmentBinaryDto>().ReverseMap();

            CreateMap<EventTeacher, EventTeacherDto>()
            .ForMember(dist => dist.TeacherName, opt => opt.MapFrom(c => c.Teacher == null ? "" : c.Teacher.Name))
                .ReverseMap()
             .ForMember(x => x.Event, op => op.Ignore())
             .ForMember(x => x.Teacher, op => op.Ignore());

            CreateMap<EventStudent, EventStudentDto>()
            .ForMember(dist => dist.EventName, opt => opt.MapFrom(c => c.Event == null ? "" : c.Event.Name))
            .ForMember(dist => dist.StudentName, opt => opt.MapFrom(c => c.Student == null ? "" : c.Student.Name))
            .ForMember(dist => dist.StudentNameAr, opt => opt.MapFrom(c => c.Student == null ? "" : c.Student.NameAr))
                .ReverseMap()
             .ForMember(x => x.Event, op => op.Ignore())
            .ForMember(x => x.Student, op => op.Ignore()).ReverseMap();
            // .ForMember(x => x.EventStudentFiles, op => op.Ignore())

            CreateMap<List<EventStudent>, List<EventStudentDto>>().ReverseMap();

            CreateMap<EventHelper, EventHelperDto>().ReverseMap();
            CreateMap<EventType, EventTypeDto>().ReverseMap();
            CreateMap<EventStudentFile, EventStudentFileDto>().ReverseMap();
            #endregion
            #region Reports
            CreateMap<ReportsHelper, ReportsHelperDto>().ReverseMap();
            #endregion
            #region ITP
            CreateMap<Itp, ItpDto>()
           .ForMember(dist => dist.StudentName, opt => opt.MapFrom(c => c.Student == null ? "" : c.Student.Name == null ? "" : c.Student.Name))
           .ForMember(dist => dist.ParamediaclName, opt => opt.MapFrom(c => c.ParamedicalService == null ? "" : c.ParamedicalService.Name == null ? "" : c.ParamedicalService.Name))
           .ForMember(dist => dist.TherapistName, opt => opt.MapFrom(c => c.Therapist == null ? "" : c.Therapist.Name == null ? "" : c.Therapist.Name))
           .ForMember(dist => dist.AcadmicYearName, opt => opt.MapFrom(c => c.AcadmicYear == null ? "" : c.AcadmicYear.Name == null ? "" : c.AcadmicYear.Name))
           .ForMember(dist => dist.TermName, opt => opt.MapFrom(c => c.Term == null ? "" : c.Term.Name == null ? "" : c.Term.Name))
                .ReverseMap()
             .ForMember(x => x.Student, op => op.Ignore())
             .ForMember(x => x.ParamedicalService, op => op.Ignore())
             .ForMember(x => x.Therapist, op => op.Ignore())
             .ForMember(x => x.AcadmicYear, op => op.Ignore())
             .ForMember(x => x.Term, op => op.Ignore())
             .ForMember(x => x.HeadOfDepartment, op => op.Ignore())
             .ForMember(x => x.HeadOfEducation, op => op.Ignore());

            CreateMap<ItpGoal, ItpGoalDto>().ReverseMap()
            .ForMember(x => x.Itp, op => op.Ignore());

            CreateMap<ItpGoalObjective, ItpGoalObjectiveDto>().ReverseMap()
           .ForMember(x => x.Itp, op => op.Ignore());

            CreateMap<ItpStrategy, ItpStrategyDto>().ReverseMap()
           .ForMember(x => x.Itp, op => op.Ignore())
           .ForMember(x => x.ParamedicalStrategy, op => op.Ignore());

            CreateMap<ParamedicalStrategy, ParamedicalStrategyDto>().ReverseMap()
           .ForMember(x => x.ParamedicalService, op => op.Ignore());

            CreateMap<ItpHelper, ItpHelperDto>().ReverseMap();
            CreateMap<ItpProgressReport, ItpProgressReportDto>().ReverseMap()
            .ForMember(x => x.Itp, op => op.Ignore())
            .ForMember(x => x.AcadmicYear, op => op.Ignore())
            .ForMember(x => x.Term, op => op.Ignore())
            .ForMember(x => x.Therapist, op => op.Ignore())
            .ForMember(x => x.Teacher, op => op.Ignore())
            .ForMember(x => x.Student, op => op.Ignore())
            .ForMember(x => x.HeadOfEducation, op => op.Ignore());

            CreateMap<ItpObjectiveProgressReport, ItpObjectiveProgressReportDto>().ReverseMap()
            .ForMember(x => x.ItpProgressReport, op => op.Ignore())
            .ForMember(x => x.ItpObjective, op => op.Ignore());

            #endregion
            #region General
            CreateMap<ApplicationGroup, ApplicationGroupDto>().ReverseMap();
            CreateMap<ApplicationGroupRole, ApplicationGroupRoleDto>().ReverseMap();
            CreateMap<ApplicationUserGroup, ApplicationUserGroupDto>().ReverseMap();
            CreateMap(typeof(Paginate<>), typeof(PaginateDto<>)).ReverseMap();
            CreateMap(typeof(List<>), typeof(List<>)).ReverseMap();
            CreateMap<Setting, SettingDto>().ReverseMap();
            #endregion

        }
    }
}
