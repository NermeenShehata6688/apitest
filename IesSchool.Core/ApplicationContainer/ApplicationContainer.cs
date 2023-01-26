using IesSchool.Context.Models;
using IesSchool.Core.IServices;
using IesSchool.Core.MappingProfile;
using IesSchool.Core.Services;
using IesSchool.InfraStructure;
using IesSchool.InfraStructure.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace IesSchool.Core
{
    public static class ApplicationContainer
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<iesContext>>();

            services.AddDbContext<iesContext>().AddUnitOfWork<iesContext>();

 
            //services.AddDbContext<iesIdentityContext>() ;

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });



            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);

            services.AddHttpContextAccessor();

            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            return services;
        }
    }
}
