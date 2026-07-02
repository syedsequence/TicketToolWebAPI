using Ticketing.Application.AuthModel;

namespace Ticketing.Application.Services.Interfaces
{
	public interface IAuthService
	{
		Task<RegisterResponse> Register(RegisterRequest register);

		Task<LoginResponse> Login(LoginRequest login);

		Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest request);

		Task<bool> ChangePassword(ChangePasswordRequest request);

		Task<bool> Logout(string userId);
	}
}
