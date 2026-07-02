namespace Ticketing.Application.AuthModel
{
	public class RegisterResponse
	{
		public string UserId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; } = "";

		public string Email { get; set; }

		public string ProfilePictureUrl { get; set; } = "";

		public string Role { get; set; }

	}
}
