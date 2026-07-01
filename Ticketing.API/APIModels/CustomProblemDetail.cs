using Microsoft.AspNetCore.Mvc;

namespace Ticketing.API.APIModels
{
	public class CustomProblemDetail : ProblemDetails
	{
		public IDictionary<string, string[]>? Errors { get; set; } = new Dictionary<string, string[]>();
	}
}
