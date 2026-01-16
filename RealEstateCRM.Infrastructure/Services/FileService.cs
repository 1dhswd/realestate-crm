using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RealEstateCRM.Application.Interfaces;
using RealEstateCRM.Application.Interfaces.Services;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateCRM.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        private const long MaxFileSize = 5 * 1024 * 1024; // 5MB

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public bool ValidateImageFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(extension))
                return false;

            if (file.Length > MaxFileSize)
                return false;

            return true;
        }

        public async Task<string> UploadImageAsync(IFormFile file, string folder = "properties")
        {
            if (!ValidateImageFile(file))
                throw new ArgumentException("Invalid image file");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var fileName = $"{Guid.NewGuid()}{extension}";

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", folder);
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{folder}/{fileName}";
        }

        public Task<bool> DeleteImageAsync(string imageUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(imageUrl))
                    return Task.FromResult(false);

                var relativePath = imageUrl.TrimStart('/');
                var filePath = Path.Combine(_env.WebRootPath, relativePath);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return Task.FromResult(true);
                }

                return Task.FromResult(false);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }
    }
}