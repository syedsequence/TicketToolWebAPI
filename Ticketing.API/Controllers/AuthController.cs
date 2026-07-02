using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using Ticketing.Application.AuthModel;
using Ticketing.Application.Constants;
using Ticketing.Application.Exceptions;
using Ticketing.Application.Response;
using Ticketing.Application.Services.Interfaces;

namespace Ticketing.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		private readonly APIResponse _apiResponse;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
			_apiResponse = new APIResponse();
		}


		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Authorize(Roles = "SUPERADMIN")]
		[HttpPost]
		[Route("Register")]
		public async Task<ActionResult<APIResponse>> Register([FromForm] RegisterRequest register)
		{

			try
			{
				if(!ModelState.IsValid)
				{
					_apiResponse.addError(ModelState.ToString());
					_apiResponse.addWarning(ApiMessages.InvalidModelState);
					return _apiResponse;
				}

				var result = await _authService.Register(register);

				_apiResponse.StatusCode = HttpStatusCode.Created;
				_apiResponse.IsSuccess = true;
				_apiResponse.data = result;
				_apiResponse.Message = AuthMessages.UserRegisteredSuccessfully;
				return _apiResponse;
			}
			catch(BadCustomException ex)
			{
				_apiResponse.StatusCode = HttpStatusCode.BadRequest;
				_apiResponse.Message = ex.Message;
			}
			catch(Exception)
			{
				_apiResponse.StatusCode = HttpStatusCode.InternalServerError;
				_apiResponse.Message = ApiMessages.InternalServerError;
			}

			return Ok(_apiResponse);

		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPost]
		[Route("Login")]
		public async Task<ActionResult<APIResponse>> Login([FromForm] LoginRequest login)
		{

			try
			{
				if(!ModelState.IsValid)
				{
					_apiResponse.addError(ModelState.ToString());
					_apiResponse.addWarning(ApiMessages.InvalidModelState);
					return _apiResponse;
				}

				var result = await _authService.Login(login);

				_apiResponse.StatusCode = HttpStatusCode.Created;
				_apiResponse.IsSuccess = true;
				_apiResponse.data = result;
				_apiResponse.Message = AuthMessages.UserLoggedInSuccessfully;
				return _apiResponse;
			}
			catch(BadCustomException ex)
			{
				_apiResponse.StatusCode = HttpStatusCode.BadRequest;
				_apiResponse.Message = ex.Message;
			}
			catch(Exception)
			{
				_apiResponse.StatusCode = HttpStatusCode.InternalServerError;
				_apiResponse.Message = ApiMessages.InternalServerError;
			}

			return Ok(_apiResponse);

		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPost]
		[Route("RefreshToken")]
		public async Task<ActionResult<APIResponse>> RefreshToken([FromBody] RefreshTokenRequest refreshToken)
		{

			try
			{
				if(!ModelState.IsValid)
				{
					_apiResponse.addError(ModelState.ToString());
					_apiResponse.addWarning(ApiMessages.InvalidModelState);
					return _apiResponse;
				}

				var result = await _authService.RefreshToken(refreshToken);

				_apiResponse.StatusCode = HttpStatusCode.OK;
				_apiResponse.IsSuccess = true;
				_apiResponse.data = result;
				_apiResponse.Message = AuthMessages.TokenGeneratedSuccessfully;
				return _apiResponse;
			}
			catch(BadCustomException ex)
			{
				_apiResponse.StatusCode = HttpStatusCode.BadRequest;
				_apiResponse.Message = ex.Message;
			}
			catch(Exception)
			{
				_apiResponse.StatusCode = HttpStatusCode.InternalServerError;
				_apiResponse.Message = ApiMessages.InternalServerError;
			}

			return Ok(_apiResponse);

		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Authorize]
		[HttpPost]
		[Route("ChangePassword")]
		public async Task<ActionResult<APIResponse>> ChangePassword([FromBody] ChangePasswordRequest changePassword)
		{

			try
			{
				if(!ModelState.IsValid)
				{
					_apiResponse.addError(ModelState.ToString());
					_apiResponse.addWarning(ApiMessages.InvalidModelState);
					return _apiResponse;
				}

				var result = await _authService.ChangePassword(changePassword);

				_apiResponse.StatusCode = HttpStatusCode.OK;
				_apiResponse.IsSuccess = true;
				_apiResponse.data = result;
				_apiResponse.Message = AuthMessages.PasswordChangedSuccessfully;
				return _apiResponse;
			}
			catch(BadCustomException ex)
			{
				_apiResponse.StatusCode = HttpStatusCode.BadRequest;
				_apiResponse.Message = ex.Message;
			}
			catch(Exception)
			{
				_apiResponse.StatusCode = HttpStatusCode.InternalServerError;
				_apiResponse.Message = ApiMessages.InternalServerError;
			}

			return Ok(_apiResponse);

		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Authorize]
		[HttpPost]
		[Route("Logout")]
		public async Task<ActionResult<APIResponse>> Logout()
		{

			try
			{
				if(!ModelState.IsValid)
				{
					_apiResponse.addError(ModelState.ToString());
					_apiResponse.addWarning(ApiMessages.InvalidModelState);
					return _apiResponse;
				}
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				var result = await _authService.Logout(userId);

				_apiResponse.StatusCode = HttpStatusCode.OK;
				_apiResponse.IsSuccess = true;
				_apiResponse.data = result;
				_apiResponse.Message = AuthMessages.UserLoggedOutSuccessfully;
				return _apiResponse;
			}
			catch(BadCustomException ex)
			{
				_apiResponse.StatusCode = HttpStatusCode.BadRequest;
				_apiResponse.Message = ex.Message;
			}
			catch(Exception)
			{
				_apiResponse.StatusCode = HttpStatusCode.InternalServerError;
				_apiResponse.Message = ApiMessages.InternalServerError;
			}

			return Ok(_apiResponse);

		}


	}
}
