using System.Net;
using Ticketing.API.APIModels;
using Ticketing.Application.Exceptions;

namespace Ticketing.API.MiddleWares
{
	public class ExceptionMiddleware
	{

		private readonly RequestDelegate _requestDelegate;
		public ExceptionMiddleware(RequestDelegate requestDelegate)
		{
			_requestDelegate = requestDelegate;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _requestDelegate(context);
			}
			catch(Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		public async Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			context.Response.ContentType = "application/json";
			HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
			CustomProblemDetail problem = new();

			switch(ex)
			{
				case BadCustomException BadCustomException:
					statusCode = HttpStatusCode.BadRequest;
					problem = new CustomProblemDetail()
					{
						Title = BadCustomException.Message,
						Status = (int)statusCode,
						Type = nameof(BadCustomException),
						Detail = BadCustomException.InnerException?.Message,
						Errors = BadCustomException.ValidationErrors
					};
					break;
			}

			context.Response.StatusCode = (int)statusCode;
			await context.Response.WriteAsJsonAsync(problem);
		}
	}
}
