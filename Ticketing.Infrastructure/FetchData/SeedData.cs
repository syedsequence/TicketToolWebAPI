using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Ticketing.Domain.Models;
using Ticketing.Infrastructure.DbContext;

namespace Ticketing.Infrastructure.FetchData
{
	public class SeedData
	{
		public static async Task SeedRoles(IServiceProvider serviceProvider)
		{
			using var scope = serviceProvider.CreateScope();
			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			var roles = new List<IdentityRole> {
				new IdentityRole{Name="SUPERADMIN",NormalizedName="SUPERADMIN" },
				new IdentityRole{Name="MANAGEMENT",NormalizedName="MANAGEMENT"},
				new IdentityRole{Name="MANAGER",NormalizedName="MANAGER"},
				new IdentityRole{Name="SUPERVISOR",NormalizedName="SUPERVISOR"},
				new IdentityRole{Name="CLIENT",NormalizedName="CLIENT"},
				new IdentityRole{Name="TECHMANAGER",NormalizedName="TECHMANAGER"},
				new IdentityRole{Name="TECHSUPERVISOR",NormalizedName="TECHSUPERVISOR"},
				new IdentityRole{Name="TECHNICIAN",NormalizedName="TECHNICIAN"}
			};
			foreach(var role in roles)
			{
				if(!await roleManager.RoleExistsAsync(role.Name))
				{
					await roleManager.CreateAsync(role);
				}
			}
		}

		public async static Task SeedAllData(TicketingDbContext _dbContext)
		{
			await SeedTickets(_dbContext);
		}

		public async static Task SeedTickets(TicketingDbContext _dbContext)
		{
			List<Ticket> tickets = [
								new Ticket {  },
								new Ticket {  }
					];

			if(!_dbContext.Tickets.Any())
			{
				foreach(var ticket in tickets)
				{
					//string random = new Random().Next(1000, 9999).ToString();
					//ticket.Code = $"CAT-{random}";
					await _dbContext.Tickets.AddAsync(ticket);
					await _dbContext.SaveChangesAsync();
				}
			}
		}
	}
}
