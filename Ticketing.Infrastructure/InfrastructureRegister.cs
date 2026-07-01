using Microsoft.Extensions.DependencyInjection;
using Ticketing.Domain.Contracts;
using Ticketing.Infrastructure.Repositories;

namespace Ticketing.Infrastructure
{
	public static class InfrastructureRegister
	{
		public static IServiceCollection AddInfrastructureService(this IServiceCollection services)
		{

			services.AddScoped(typeof(ICommonRepo<>), typeof(CommonRepo<>));


			return services;
		}
	}
}
