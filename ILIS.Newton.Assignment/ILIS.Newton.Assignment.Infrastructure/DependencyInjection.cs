using ILIS.Newton.Assignment.DataAccess;
using ILIS.Newton.Assignment.Infrastructure.Repositories.Abstraction;
using ILIS.Newton.Assignment.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ILIS.Newton.Assignment.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IVideoGamesRepository, VideoGamesRepository>();

            return services;
        }
    }
}
