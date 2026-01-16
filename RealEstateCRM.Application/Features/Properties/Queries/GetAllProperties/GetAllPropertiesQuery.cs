using MediatR;
using RealEstateCRM.Application.DTOs.Property;
using System.Collections.Generic;

namespace RealEstateCRM.Application.Features.Properties.Queries.GetAllProperties
{
    public class GetAllPropertiesQuery : IRequest<List<PropertyDto>>
    {
        public string City { get; set; }
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? OnlyActive { get; set; }
    }
}