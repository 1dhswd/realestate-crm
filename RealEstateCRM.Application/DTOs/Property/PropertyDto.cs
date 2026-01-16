using System;
using System.Collections.Generic;

namespace RealEstateCRM.Application.DTOs.Property
{
    public class PropertyDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Area { get; set; }
        public int? RoomCount { get; set; }
        public int? BathroomCount { get; set; }
        public int? FloorNumber { get; set; }
        public int? BuildingAge { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }
        public int ViewCount { get; set; }
        public DateTime? PublishedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public string CategoryName { get; set; }
        public string TypeName { get; set; }
        public string OwnerName { get; set; }
        public List<string> Features { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}