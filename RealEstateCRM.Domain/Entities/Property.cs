using RealEstateCRM.Domain.Common;
using System;
using System.Collections.Generic;

namespace RealEstateCRM.Domain.Entities
{
    public class Property : BaseEntity
    {
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

        public int CategoryId { get; set; }
        public int TypeId { get; set; }
        public int OwnerId { get; set; }

        public PropertyCategory Category { get; set; }
        public PropertyType Type { get; set; }
        public User Owner { get; set; }
        public ICollection<PropertyImage> Images { get; set; }
        public ICollection<PropertyPropertyFeature> PropertyFeatures { get; set; }
        public ICollection<Lead> Leads { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Offer> Offers { get; set; }

        public Property()
        {
            Images = new HashSet<PropertyImage>();
            PropertyFeatures = new HashSet<PropertyPropertyFeature>();
            Leads = new HashSet<Lead>();
            Appointments = new HashSet<Appointment>();
            Offers = new HashSet<Offer>();
            IsActive = true;
            IsFeatured = false;
            ViewCount = 0;
        }
    }
}