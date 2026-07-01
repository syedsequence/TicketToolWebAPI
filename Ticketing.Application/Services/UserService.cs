using Ticketing.Application.Services.Interfaces;
using Ticketing.Application.UserModel;

namespace Ticketing.Application.Services
{
	public class UserService : IUserService
	{
		public Task<UserProfileResponse> GetProfile(string _userId)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateProfile(UpdateUserRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
