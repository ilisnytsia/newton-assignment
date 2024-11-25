using ILIS.Newton.Assignment.Application.MappingProfiles;
using ILIS.Newton.Assignment.Application.Services;
using ILIS.Newton.Assignment.Application.Services.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace ILIS.Newton.Assignment.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(VideoGameProfile));

            services.AddScoped<IVideoGameService, VideoGameService>()
;
            return services;
        }
    }
}
