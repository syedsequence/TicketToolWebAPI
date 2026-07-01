using System.Net;

namespace Ticketing.Application.Response
{
	public class APIResponse
	{
		public HttpStatusCode StatusCode { get; set; }

		public bool IsSuccess { get; set; } = false;

		public string Message { get; set; } = "";

		public object data { get; set; }

		public List<APIError> Errors { get; set; } = new();

		public List<APIWarning> Warnings { get; set; } = new();

		public void addError(string error)
		{
			if(Errors == null)
				Errors = new List<APIError>();
			Errors.Add(new APIError(error: error));
		}

		public void addWarning(string warning)
		{
			if(Warnings == null)
				Warnings = new List<APIWarning>();
			Warnings.Add(new APIWarning(warning: warning));
		}
	}
}
