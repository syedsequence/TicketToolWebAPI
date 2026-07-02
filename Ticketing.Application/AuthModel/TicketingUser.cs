using Microsoft.AspNetCore.Identity;

namespace Ticketing.Application.AuthModel
{
	public class TicketingUser : IdentityUser
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string ProfilePictureUrl { get; set; }

		public DateTime? DateOfBirth { get; set; }

		public DateTime LastLoginTime { get; set; }

		public string RefreshToken { get; set; }

		public DateTime RefreshTokenExpiryTime { get; set; } = DateTime.MinValue;

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	}
}
