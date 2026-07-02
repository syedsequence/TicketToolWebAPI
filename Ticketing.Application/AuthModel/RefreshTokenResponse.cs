namespace Ticketing.Application.AuthModel
{
	public class RefreshTokenResponse
	{

		public string UserId { get; set; }
		public string Token { get; set; }

		public string RefreshToken { get; set; }

		public DateTime RefreshTokenExpiration { get; set; }
	}
}
