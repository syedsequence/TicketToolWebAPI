using System.ComponentModel.DataAnnotations;

namespace Ticketing.Application.UserModel
{
	public class UpdateUserRequest
	{
		[Required]
		public string Id { get; set; }

		[Required]
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string PhoneNumber { get; set; }

		public string ProfilePictureUrl { get; set; }
	}
}
