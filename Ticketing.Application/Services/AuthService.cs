using Microsoft.AspNetCore.Identity;
using Ticketing.Application.AuthModel;
using Ticketing.Application.Services.Interfaces;

namespace Ticketing.Application.Services
{
	public class AuthService : IAuthService
	{
		public Task<bool> ChangePassword(ChangePasswordRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<LoginResponse> Login(LoginRequest login)
		{
			throw new NotImplementedException();
		}

		public Task<bool> Logout()
		{
			throw new NotImplementedException();
		}

		public Task<LoginResponse> RefreshToken(RefreshTokenRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<IdentityError>> Register(RegisterRequest register)
		{
			throw new NotImplementedException();
		}
	}
}
