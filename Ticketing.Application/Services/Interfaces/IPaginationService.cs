using Ticketing.Application.Pagination;

namespace Ticketing.Application.Services.Interfaces
{
	public interface IPaginationService<T, S> where T : class
	{
		PaginationResponse<T> GetPagination(PaginationRequest pagination, IEnumerable<S> source);
	}
}
