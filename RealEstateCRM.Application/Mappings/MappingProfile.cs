using AutoMapper;
using RealEstateCRM.Application.DTOs.Appointment;
using RealEstateCRM.Application.DTOs.Auth;
using RealEstateCRM.Application.DTOs.Customer;
using RealEstateCRM.Application.DTOs.Lead;
using RealEstateCRM.Application.DTOs.Property;
using RealEstateCRM.Application.DTOs.User;
using RealEstateCRM.Domain.Entities;
using System.Linq;

namespace RealEstateCRM.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Property, PropertyDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Type.Name))
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner.FirstName + " " + src.Owner.LastName))
                .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.PropertyFeatures.Select(pf => pf.Feature.Name).ToList()))
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images.OrderBy(i => i.DisplayOrder).Select(i => i.ImageUrl).ToList()));

            CreateMap<CreatePropertyDto, Property>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.PublishedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ViewCount, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.IsFeatured, opt => opt.MapFrom(src => false));

            CreateMap<UpdatePropertyDto, Property>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.PublishedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ViewCount, opt => opt.Ignore())
                .ForMember(dest => dest.OwnerId, opt => opt.Ignore());

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name).ToList()));

            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.LastLoginAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.AssignedAgentName, opt => opt.MapFrom(src =>
                    src.AssignedAgent != null ? src.AssignedAgent.FirstName + " " + src.AssignedAgent.LastName : null));

            CreateMap<CreateCustomerDto, Customer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dest => dest.LeadName, opt => opt.MapFrom(src => src.Lead.Customer.FirstName + " " + src.Lead.Customer.LastName))
                .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => src.Property.Title))
                .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.Agent.FirstName + " " + src.Agent.LastName));

            CreateMap<Lead, LeadDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName))
                .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => src.Property != null ? src.Property.Title : null))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.StatusColor, opt => opt.MapFrom(src => src.Status.ColorCode))
                .ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByUser.FirstName + " " + src.CreatedByUser.LastName));

            CreateMap<Customer, CustomerDto>()
    .ForMember(dest => dest.AssignedAgentName, opt =>
        opt.MapFrom(src =>
            src.AssignedAgent != null
                ? src.AssignedAgent.FirstName + " " + src.AssignedAgent.LastName
                : null))
    .ForMember(dest => dest.StatusName, opt =>
        opt.MapFrom(src =>
            src.Status != null ? src.Status.Name : null));


            CreateMap<CreateLeadDto, Lead>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ClosedAt, opt => opt.Ignore());

            CreateMap<UpdateLeadDto, Lead>()
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
                .ForMember(dest => dest.PropertyId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedByUserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.NextFollowUpDate, opt => opt.Condition(src => src.NextFollowUpDate.HasValue)
    );
        }
    }
}