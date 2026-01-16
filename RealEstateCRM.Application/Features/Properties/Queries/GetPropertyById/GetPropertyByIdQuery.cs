using MediatR;
using RealEstateCRM.Application.DTOs.Property;

namespace RealEstateCRM.Application.Features.Properties.Queries.GetPropertyById
{
    public class GetPropertyByIdQuery : IRequest<PropertyDto>
    {
        public int Id { get; set; }

        public GetPropertyByIdQuery(int id)
        {
            Id = id;
        }
    }
}