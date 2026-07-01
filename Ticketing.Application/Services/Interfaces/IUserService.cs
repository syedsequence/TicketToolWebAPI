using Ticketing.Application.UserModel;

namespace Ticketing.Application.Services.Interfaces
{
	public interface IUserService
	{
		Task<UserProfileResponse> GetProfile(string _userId);

		Task<bool> UpdateProfile(UpdateUserRequest request);
	}
}
