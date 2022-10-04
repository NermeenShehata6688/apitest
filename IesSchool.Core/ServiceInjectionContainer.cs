using IesSchool.Context.Models;
using IesSchool.Core.IServices;
using IesSchool.Core.MappingProfile;
using IesSchool.Core.Services;
using IesSchool.InfraStructure;
using IesSchool.InfraStructure.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace IesSchool.Core
{
    public static class ServiceInjectionContainer
    {
        public static IServiceCollection ServiceInjection(this IServiceCollection services)
        {
            services.AddScoped<IUserApplicationGroupService, UserApplicationGroupService>();
            services.AddScoped<ILocationService, LocationServices>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ILocationService, LocationServices>();
            services.AddScoped<IExtraCurricularService, ExtraCurricularService>();
            services.AddScoped<IParamedicalServicesService, ParamedicalServicesService>();
            services.AddScoped<ISkillEvaluationService, SkillEvaluationService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IAcadmicYearsService, AcadmicYearsService>();
            services.AddScoped<ITermsService, TermsService>();
            services.AddScoped<ICurriculumService, CurriculumService>();
            services.AddScoped<IAssistantService, AssistantService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAttachmentTypeService, AttachmentTypeService>();
            services.AddScoped<IIepService, IepService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IImportExcelToSqlService, ImportExcelToSqlService>();
            services.AddScoped<ILogCommentService, LogCommentService>();
            services.AddScoped<IItpService, ItpService>();
            services.AddScoped<IIxpService, IxpService>(); 
            services.AddScoped<IMobileService, MobileService>(); 
            services.AddScoped<IStatisticService, StatisticService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddScoped<IProgramsService, ProgramsService>(); 
            services.AddScoped<IParentTokenService, ParentTokenService>(); 

            return services; 
        }
    }
}
