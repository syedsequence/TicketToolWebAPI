using Ticketing.Domain.Common;
using Ticketing.Domain.Contracts;

namespace Ticketing.Infrastructure.Repositories
{
	public class CommonRepo<T> : ICommonRepo<T> where T : BaseModel
	{
	}
}
