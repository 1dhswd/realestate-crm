using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Interfaces.Services
{
    public interface IFileService
    {
        Task<string> UploadImageAsync(IFormFile file, string folder = "properties");
        Task<bool> DeleteImageAsync(string imageUrl);
        bool ValidateImageFile(IFormFile file);
    }
}