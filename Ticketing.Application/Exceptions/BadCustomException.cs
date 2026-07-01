

using FluentValidation.Results;

namespace Ticketing.Application.Exceptions
{
	public class BadCustomException : Exception
	{
		public IDictionary<string, string[]> ValidationErrors { get; set; }
		public BadCustomException(string message) : base(message)
		{

		}
		public BadCustomException(string message, ValidationResult validation) : base(message)
		{
			ValidationErrors = validation.ToDictionary();
		}
	}
}
