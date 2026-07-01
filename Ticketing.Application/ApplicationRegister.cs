using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Ticketing.Application.Mapping;
using Ticketing.Application.Services;
using Ticketing.Application.Services.Interfaces;

namespace Ticketing.Application
{
	public static class ApplicationRegister
	{
		public static IServiceCollection AddApplicationService(this IServiceCollection services)
		{
			var config = TypeAdapterConfig.GlobalSettings;
			config.Scan(typeof(MappingService).Assembly);

			services.AddSingleton(config);
			services.AddScoped<IMapper, ServiceMapper>();
			services.AddScoped(typeof(IPaginationService<,>), typeof(PaginationService<,>));

			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IImageService, ImageService>();

			return services;
		}
	}
}
