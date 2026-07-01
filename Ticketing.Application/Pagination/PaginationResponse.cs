namespace Ticketing.Application.Pagination
{
	public class PaginationResponse<T>
	{
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public int TotalRecords { get; set; }
		public int PageSize { get; set; }
		public bool NextPage => CurrentPage < TotalPages;
		public bool PreviousPage => CurrentPage > 1;
		public IEnumerable<T> Items { get; set; }

		public PaginationResponse(int currentPage, int totalPages, int totalRecords, int pageSize, IEnumerable<T> items)
		{
			CurrentPage = currentPage;
			TotalPages = totalPages;
			TotalRecords = totalRecords;
			PageSize = pageSize;
			Items = items;
		}
	}
}
