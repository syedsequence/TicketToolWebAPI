using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Ticketing.Application.Constants;
using Ticketing.Application.Exceptions;
using Ticketing.Application.Services.Interfaces;

namespace Ticketing.Application.Services
{
	public class ImageService : IImageService
	{

		private readonly IWebHostEnvironment _environment;

		public ImageService(IWebHostEnvironment environment)
		{
			_environment = environment;
		}

		public async Task<string?> UploadImageAsync(IFormFile file, string folderName)
		{
			if(file == null || file.Length == 0)
				throw new BadCustomException(CommonMessages.SelectImage);

			// Allowed extensions
			var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

			var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

			if(!allowedExtensions.Contains(extension))
				throw new BadCustomException(CommonMessages.InvalidImageExtension);

			var allowedContentTypes = new[]
			{
			"image/jpeg",
			"image/png"
			};

			if(!allowedContentTypes.Contains(file.ContentType.ToLower()))
			{
				throw new BadCustomException(CommonMessages.InvalidImageFormat);
			}

			const long minSize = 50 * 1024;
			const long maxSize = 200 * 1024;

			if(file.Length < minSize || file.Length > maxSize)
			{
				throw new BadCustomException(CommonMessages.InvalidImageSize);
			}

			var webRoot = _environment.WebRootPath;

			if(string.IsNullOrWhiteSpace(webRoot))
			{
				webRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
			}

			Directory.CreateDirectory(webRoot);

			var uploadsFolder = Path.Combine(webRoot, "Uploads", folderName);

			Directory.CreateDirectory(uploadsFolder);



			var fileName = $"{Guid.NewGuid()}{extension}";

			var path = Path.Combine(uploadsFolder, fileName);

			using var stream = new FileStream(path, FileMode.Create);

			await file.CopyToAsync(stream);

			return $"Uploads/{folderName}/{fileName}";
		}
	}
}
