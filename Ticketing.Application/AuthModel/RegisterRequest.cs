using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ticketing.Application.AuthModel
{
	public class RegisterRequest
	{
		[Required]
		public string FirstName { get; set; }

		public string LastName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		[Compare(nameof(Password))]
		public string ConfirmPassword { get; set; }

		public IFormFile? ProfileImage { get; set; }
	}
}
