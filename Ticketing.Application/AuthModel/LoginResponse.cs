namespace Ticketing.Application.AuthModel
{
	public class LoginResponse
	{
		public string UserId { get; set; }

		public DateTime LastLoggedIn { get; set; }

		public string Token { get; set; }

		public string RefreshToken { get; set; }

		public DateTime RefreshTokenExpiryTime { get; set; }

		public string Role { get; set; }

	}
}
