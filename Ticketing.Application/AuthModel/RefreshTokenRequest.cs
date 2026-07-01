using System.ComponentModel.DataAnnotations;

namespace Ticketing.Application.AuthModel
{
	public class RefreshTokenRequest
	{

		[Required]
		public string UserId { get; set; }
		[Required]
		public string Token { get; set; }

		[Required]
		public string RefreshToken { get; set; }

	}
}
