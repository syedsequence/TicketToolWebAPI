using Microsoft.AspNetCore.Http;

namespace Ticketing.Application.Services.Interfaces
{
	public interface IImageService
	{
		Task<string> UploadImageAsync(IFormFile file, string folderName);
	}
}
