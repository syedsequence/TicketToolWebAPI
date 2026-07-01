using Microsoft.AspNetCore.Identity;
using Ticketing.Application.AuthModel;

namespace Ticketing.Application.Services.Interfaces
{
	public interface IAuthService
	{
		Task<IEnumerable<IdentityError>> Register(RegisterRequest register);

		Task<LoginResponse> Login(LoginRequest login);

		Task<LoginResponse> RefreshToken(RefreshTokenRequest request);

		Task<bool> ChangePassword(ChangePasswordRequest request);

		Task<bool> Logout();
	}
}
