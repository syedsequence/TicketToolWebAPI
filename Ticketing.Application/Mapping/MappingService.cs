using Mapster;
using Ticketing.Application.Dto.TicketDto;
using Ticketing.Domain.Models;

namespace Ticketing.Application.Mapping
{
	public class MappingService : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			// TICKET
			config.NewConfig<Ticket, TicketDto>().TwoWays();
			config.NewConfig<Ticket, CreateTicketDto>().TwoWays();
			config.NewConfig<Ticket, UpdateTicketDto>().TwoWays();
		}
	}
}
