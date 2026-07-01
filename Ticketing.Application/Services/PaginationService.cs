using Mapster;
using Ticketing.Application.Pagination;
using Ticketing.Application.Services.Interfaces;

namespace Ticketing.Application.Services
{
	public class PaginationService<T, S> : IPaginationService<T, S> where T : class
	{


		public PaginationResponse<T> GetPagination(PaginationRequest pagination, IEnumerable<S> source)
		{
			var currentPage = pagination.PageNumber;
			var pageSize = pagination.PageSize;
			var totalRecords = source.Count();
			var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
			var items = source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
			var result = items.Adapt<IEnumerable<T>>();

			PaginationResponse<T> paginationOutput = new PaginationResponse<T>(currentPage, totalPages, totalRecords, pageSize, result);
			return paginationOutput;
		}
	}
}
