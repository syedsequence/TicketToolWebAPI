using System.ComponentModel.DataAnnotations;

namespace Ticketing.Application.AuthModel
{
	public class ChangePasswordRequest
	{
		[Required]
		public string UserId { get; set; }

		[Required]
		public string CurrentPassword { get; set; }

		[Required]
		public string NewPassword { get; set; }

		[Required]
		[Compare(nameof(NewPassword))]
		public string ConfirmPassword { get; set; }
	}
}
