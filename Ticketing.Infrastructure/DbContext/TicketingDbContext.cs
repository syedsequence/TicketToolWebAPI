using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ticketing.Application.AuthModel;
using Ticketing.Domain.Models;

namespace Ticketing.Infrastructure.DbContext
{
	public class TicketingDbContext : IdentityDbContext<TicketingUser>
	{
		public TicketingDbContext(DbContextOptions<TicketingDbContext> options) : base(options)
		{

		}
		public DbSet<Ticket> Tickets { get; set; }
	}
}
