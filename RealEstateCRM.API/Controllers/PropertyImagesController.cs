using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateCRM.Application.DTOs.Property;
using RealEstateCRM.Application.Interfaces.Services;
using RealEstateCRM.Domain.Entities;
using RealEstateCRM.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateCRM.API.Controllers
{
    [Route("api/properties/{propertyId}/images")]
    [ApiController]
    public class PropertyImagesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;

        public PropertyImagesController(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyImageDto>>> GetPropertyImages(int propertyId)
        {
            var images = await _context.PropertyImages
                .Where(pi => pi.PropertyId == propertyId)
                .OrderBy(pi => pi.DisplayOrder)
                .ThenByDescending(pi => pi.IsMainImage)
                .Select(pi => new PropertyImageDto
                {
                    Id = pi.Id,
                    PropertyId = pi.PropertyId,
                    ImageUrl = pi.ImageUrl,
                    FileName = pi.FileName,
                    FileSize = pi.FileSize,
                    IsPrimary = pi.IsMainImage,
                    DisplayOrder = pi.DisplayOrder,
                    CreatedAt = pi.CreatedAt
                })
                .ToListAsync();

            return Ok(images);
        }

        [HttpPost]
        public async Task<ActionResult> UploadImages(int propertyId, [FromForm] List<IFormFile> files)
        {
            var property = await _context.Properties.FindAsync(propertyId);
            if (property == null)
                return NotFound(new { message = "Property not found" });

            if (files == null || files.Count == 0)
                return BadRequest(new { message = "No files uploaded" });

            var uploadedImages = new List<PropertyImageDto>();
            var existingImagesCount = await _context.PropertyImages
                .CountAsync(pi => pi.PropertyId == propertyId);

            foreach (var file in files)
            {
                try
                {
                    if (!_fileService.ValidateImageFile(file))
                    {
                        return BadRequest(new { message = $"Invalid file: {file.FileName}. Only JPG, PNG, GIF, WEBP allowed (max 5MB)" });
                    }

                    var imageUrl = await _fileService.UploadImageAsync(file, "properties");

                    var propertyImage = new PropertyImage
                    {
                        PropertyId = propertyId,
                        ImageUrl = imageUrl,
                        FileName = file.FileName,
                        FileSize = file.Length,
                        IsMainImage = existingImagesCount == 0 && uploadedImages.Count == 0,
                        DisplayOrder = existingImagesCount + uploadedImages.Count,
                        UploadedAt = DateTime.UtcNow
                    };

                    _context.PropertyImages.Add(propertyImage);
                    await _context.SaveChangesAsync();

                    uploadedImages.Add(new PropertyImageDto
                    {
                        Id = propertyImage.Id,
                        PropertyId = propertyImage.PropertyId,
                        ImageUrl = propertyImage.ImageUrl,
                        FileName = propertyImage.FileName,
                        FileSize = propertyImage.FileSize,
                        IsPrimary = propertyImage.IsMainImage,
                        DisplayOrder = propertyImage.DisplayOrder,
                        CreatedAt = propertyImage.CreatedAt
                    });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = $"Failed to upload {file.FileName}: {ex.Message}" });
                }
            }

            return Ok(new
            {
                message = $"{uploadedImages.Count} image(s) uploaded successfully",
                images = uploadedImages
            });
        }

        [HttpPut("{imageId}/set-primary")]
        public async Task<IActionResult> SetPrimaryImage(int propertyId, int imageId)
        {
            var image = await _context.PropertyImages
                .FirstOrDefaultAsync(pi => pi.Id == imageId && pi.PropertyId == propertyId);

            if (image == null)
                return NotFound(new { message = "Image not found" });

            var allImages = await _context.PropertyImages
                .Where(pi => pi.PropertyId == propertyId)
                .ToListAsync();

            foreach (var img in allImages)
            {
                img.IsMainImage = img.Id == imageId;
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Primary image updated" });
        }

        [HttpPut("reorder")]
        public async Task<IActionResult> ReorderImages(int propertyId, [FromBody] List<int> imageIds)
        {
            var images = await _context.PropertyImages
                .Where(pi => pi.PropertyId == propertyId && imageIds.Contains(pi.Id))
                .ToListAsync();

            for (int i = 0; i < imageIds.Count; i++)
            {
                var image = images.FirstOrDefault(img => img.Id == imageIds[i]);
                if (image != null)
                {
                    image.DisplayOrder = i;
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Images reordered successfully" });
        }

        [HttpDelete("{imageId}")]
        public async Task<IActionResult> DeleteImage(int propertyId, int imageId)
        {
            var image = await _context.PropertyImages
                .FirstOrDefaultAsync(pi => pi.Id == imageId && pi.PropertyId == propertyId);

            if (image == null)
                return NotFound(new { message = "Image not found" });

            await _fileService.DeleteImageAsync(image.ImageUrl);

            _context.PropertyImages.Remove(image);

            if (image.IsMainImage)
            {
                var newPrimary = await _context.PropertyImages
                    .Where(pi => pi.PropertyId == propertyId && pi.Id != imageId)
                    .OrderBy(pi => pi.DisplayOrder)
                    .FirstOrDefaultAsync();

                if (newPrimary != null)
                {
                    newPrimary.IsMainImage = true;
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Image deleted successfully" });
        }
    }
}