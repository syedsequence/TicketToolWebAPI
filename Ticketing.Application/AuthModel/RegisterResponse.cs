namespace Ticketing.Application.AuthModel
{
	public class RegisterResponse
	{
		public string UserId { get; set; }

		public string Token { get; set; }

		public string RefreshToken { get; set; }

		public DateTime RefreshTokenExpiryTime { get; set; }

		public bool EmailVerified { get; set; } = false;

		public bool PhoneVerified { get; set; } = false;

	}
}
