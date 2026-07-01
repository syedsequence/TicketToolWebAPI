using Microsoft.AspNetCore.Mvc;
using System.Net;
using Ticketing.Application.AuthModel;
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
		[HttpPost]
		[Route("Register")]
		public async Task<ActionResult<APIResponse>> Register([FromForm] RegisterRequest register)
		{

			try
			{
				if(!ModelState.IsValid)
				{
					_apiResponse.addError(ModelState.ToString());
					_apiResponse.addWarning("");
					return _apiResponse;
				}

				var result = await _authService.Register(register);

				_apiResponse.IsSuccess = true;
				_apiResponse.data = result;
				_apiResponse.Message = "";
				return _apiResponse;
			}
			catch(Exception)
			{
				_apiResponse.StatusCode = HttpStatusCode.InternalServerError;
				_apiResponse.Message = "";
			}

			return Ok(_apiResponse);

		}

	}
}
