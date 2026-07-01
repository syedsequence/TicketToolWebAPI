using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
				throw new Exception("Please select an image.");

			// Allowed extensions
			var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

			var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

			if(!allowedExtensions.Contains(extension))
				throw new Exception("Only JPG, JPEG and PNG images are allowed.");

			var allowedContentTypes = new[]
			{
			"image/jpeg",
			"image/png"
			};

			if(!allowedContentTypes.Contains(file.ContentType.ToLower()))
			{
				throw new Exception("Invalid image format.");
			}

			const long minSize = 50 * 1024;
			const long maxSize = 200 * 1024;

			if(file.Length < minSize || file.Length > maxSize)
			{
				throw new Exception("Image size must be between 50 KB and 200 KB.");
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
