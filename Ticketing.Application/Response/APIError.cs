namespace Ticketing.Application.Response
{
	public class APIError
	{
		public string ErrorMessage { get; set; }
		public APIError(string error)
		{
			ErrorMessage = error;

		}
	}
}
