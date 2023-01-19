using IesSchool.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace RealEstate.Context
{
    public static class PersistenceContainer
    {
     

        public static IServiceCollection AddPersistenceServices(this IServiceCollection services , string conection)
        {
            services.AddDbContext<iesContext>(options =>
                options.UseSqlServer(conection, options => options.CommandTimeout(180))
                 ); 
            //services.AddDbContext<iesIdentityContext>(options =>
            //    options.UseSqlServer(conection));

            //services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            //services.AddScoped(typeof(IPostRepository), typeof(PostRepository));

            return services;
        }
    }

}