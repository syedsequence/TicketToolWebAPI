using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Ticketing.Application.AuthModel;
using Ticketing.Application.Constants;
using Ticketing.Application.Exceptions;
using Ticketing.Application.Services.Interfaces;

namespace Ticketing.Application.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<TicketingUser> _userManager;
		private readonly SignInManager<TicketingUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IImageService _imageService;
		protected IConfiguration _config;
		private TicketingUser TicketUser;

		public AuthService(UserManager<TicketingUser> userManager, SignInManager<TicketingUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration config, IImageService imageService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_config = config;
			_imageService = imageService;
			TicketUser = new TicketingUser();
		}

		public async Task<RegisterResponse> Register(RegisterRequest register)
		{
			try
			{
				var user = await _userManager.FindByEmailAsync(register.Email);

				if(user != null)
				{
					throw new BadCustomException(AuthMessages.EmailAlreadyExists);
				}
				TicketUser.FirstName = register.FirstName;
				TicketUser.LastName = register.LastName;
				TicketUser.Email = register.Email;
				TicketUser.UserName = register.Email;

				var IsRoleExists = await _roleManager.RoleExistsAsync(register.Role);
				if(!IsRoleExists)
				{
					throw new BadCustomException(AuthMessages.RoleNotFound);
				}
				if(register.ProfileImage != null)
				{
					TicketUser.ProfilePictureUrl =
						await _imageService.UploadImageAsync(
							register.ProfileImage,
							"ProfileImages");
				}
				var result = await _userManager.CreateAsync(TicketUser, register.Password);
				if(result.Succeeded)
				{

					await _userManager.AddToRoleAsync(TicketUser, register.Role);
					return new RegisterResponse
					{
						UserId = TicketUser.Id,
						FirstName = TicketUser.FirstName,
						LastName = TicketUser.LastName,
						Email = TicketUser.Email,
						ProfilePictureUrl = TicketUser.ProfilePictureUrl ?? "",
						Role = register.Role
					};


				}
				else
				{
					throw new BadCustomException(AuthMessages.UserRegistrationFailed);
				}
			}
			catch(BadCustomException ex)
			{
				throw new BadCustomException(ex.Message);
			}
			catch(Exception ex)
			{
				throw new BadCustomException(ex.Message);
			}
		}


		public async Task<LoginResponse> Login(LoginRequest login)
		{
			try
			{
				var user = await _userManager.FindByEmailAsync(login.Email);

				if(user == null)
				{
					throw new BadCustomException(AuthMessages.UserNotFound);
				}

				var result = await _signInManager.CheckPasswordSignInAsync(
					user,
					login.Password,
					false);

				if(!result.Succeeded)
				{
					throw new BadCustomException(AuthMessages.InvalidPassword);
				}

				TicketUser = user;

				var token = await GenerateToken();
				var refreshToken = await GenerateRefreshToken();

				user.RefreshToken = refreshToken;
				user.LastLoginTime = DateTime.UtcNow;
				user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(Convert.ToDouble(_config["Jwt:RefreshTokenDurationInDays"]));

				await _userManager.UpdateAsync(user);
				var roles = await _userManager.GetRolesAsync(user);

				return new LoginResponse
				{
					UserId = user.Id,
					LastLoggedIn = user.LastLoginTime,
					Token = token,
					RefreshToken = refreshToken,
					RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
					Role = roles.FirstOrDefault()
				};
			}
			catch(BadCustomException ex)
			{
				throw new BadCustomException(ex.Message);
			}
			catch(Exception ex)
			{
				throw new BadCustomException(ex.Message);
			}
		}


		public async Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest request)
		{
			try
			{

				var user = await _userManager.FindByIdAsync(request.UserId);

				if(user == null)
					throw new BadCustomException(AuthMessages.UserNotFound);

				await GetPrincipalFromExpiredToken(request.Token);

				if(user.RefreshToken != request.RefreshToken)
					throw new BadCustomException(AuthMessages.InvalidRefreshToken);

				if(user.RefreshTokenExpiryTime <= DateTime.UtcNow)
					throw new BadCustomException(AuthMessages.RefreshTokenExpired);

				TicketUser = user;

				var newToken = await GenerateToken();

				var newRefreshToken = await GenerateRefreshToken();

				user.RefreshToken = newRefreshToken;
				user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

				await _userManager.UpdateAsync(user);

				var roles = await _userManager.GetRolesAsync(user);

				return new RefreshTokenResponse
				{
					UserId = user.Id,
					Token = newToken,
					RefreshToken = newRefreshToken,
					RefreshTokenExpiration = user.RefreshTokenExpiryTime
				};
			}
			catch(BadCustomException ex)
			{
				throw new BadCustomException(ex.Message);
			}
			catch(Exception ex)
			{
				throw new BadCustomException(ex.Message);
			}
		}

		public async Task<bool> ChangePassword(ChangePasswordRequest request)
		{
			try
			{
				if(request.NewPassword != request.ConfirmPassword)
					throw new BadCustomException(AuthMessages.PasswordDoesNotMatch);

				var user = await _userManager.FindByIdAsync(request.UserId);

				if(user == null)
					throw new BadCustomException(AuthMessages.UserNotFound);

				var result = await _userManager.ChangePasswordAsync(
					user,
					request.CurrentPassword,
					request.NewPassword);

				return result.Succeeded;
			}
			catch(BadCustomException ex)
			{
				throw new BadCustomException(AuthMessages.PasswordChangeFailed);
			}
			catch(Exception ex)
			{
				throw new BadCustomException(ex.Message);
			}
		}


		public async Task<bool> Logout(string userId)
		{
			try
			{
				var user = await _userManager.FindByIdAsync(userId);


				if(user == null)
					throw new BadCustomException(AuthMessages.UserNotFound);

				if(user.RefreshToken == null)
					throw new BadCustomException(AuthMessages.UserAlreadyLoggedOut);
				user.RefreshToken = null;
				user.RefreshTokenExpiryTime = DateTime.UtcNow;

				var result = await _userManager.UpdateAsync(user);

				return result.Succeeded;
			}
			catch(BadCustomException ex)
			{
				throw new BadCustomException(AuthMessages.UserLogoutFailed);
			}
			catch(Exception ex)
			{
				throw new BadCustomException(ex.Message);
			}
		}


		private async Task<string> GenerateToken()
		{
			try
			{
				var securityKey = new SymmetricSecurityKey(
							 Encoding.UTF8.GetBytes(_config["MyCustomSettings:APIKEY"]));

				var credentials = new SigningCredentials(
					securityKey,
					SecurityAlgorithms.HmacSha256);

				var roles = await _userManager.GetRolesAsync(TicketUser);

				List<Claim> claims = new()
					{
						new Claim(JwtRegisteredClaimNames.Sub, TicketUser.Id),
						new Claim(JwtRegisteredClaimNames.Email, TicketUser.Email),
						new Claim(JwtRegisteredClaimNames.UniqueName, TicketUser.UserName),
						new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
						new Claim(ClaimTypes.NameIdentifier, TicketUser.Id),
						new Claim(ClaimTypes.Name, TicketUser.UserName),
						new Claim(ClaimTypes.Email,TicketUser.Email)
					};

				foreach(var role in roles)
				{
					claims.Add(new Claim(ClaimTypes.Role, role));
				}

				var token = new JwtSecurityToken(
					issuer: _config["Jwt:Issuer"],
					audience: _config["Jwt:Audience"],
					claims: claims,
					expires: DateTime.UtcNow.AddMinutes(
						Convert.ToDouble(_config["Jwt:DurationInMinutes"])),
					signingCredentials: credentials);

				return new JwtSecurityTokenHandler().WriteToken(token);
			}
			catch(BadCustomException ex)
			{
				throw new BadCustomException(AuthMessages.TokenGenerationFailed);
			}
		}


		private async Task<string> GenerateRefreshToken()
		{
			try
			{
				var randomNumber = new byte[64];

				using var rng = RandomNumberGenerator.Create();
				rng.GetBytes(randomNumber);

				return Convert.ToBase64String(randomNumber);
			}
			catch(BadCustomException ex)
			{
				throw new BadCustomException(AuthMessages.RefreshTokenGenerationFailed);
			}

		}


		private async Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
		{
			try
			{
				var tokenValidationParameters = new TokenValidationParameters
				{
					ValidateAudience = true,
					ValidateIssuer = true,
					ValidateIssuerSigningKey = true,
					ValidateLifetime = false,

					ValidIssuer = _config["Jwt:Issuer"],
					ValidAudience = _config["Jwt:Audience"],

					IssuerSigningKey = new SymmetricSecurityKey(
						Encoding.UTF8.GetBytes(_config["MyCustomSettings:APIKEY"]))
				};

				var tokenHandler = new JwtSecurityTokenHandler();

				var principal = tokenHandler.ValidateToken(
					token,
					tokenValidationParameters,
					out SecurityToken securityToken);

				if(securityToken is not JwtSecurityToken jwt ||
					!jwt.Header.Alg.Equals(
						SecurityAlgorithms.HmacSha256,
						StringComparison.InvariantCultureIgnoreCase))
				{
					throw new BadCustomException(AuthMessages.InvalidToken);
				}

				return principal;
			}
			catch(BadCustomException ex)
			{
				throw new BadCustomException(AuthMessages.TokenVerificationFailed);
			}
		}


	}
}
