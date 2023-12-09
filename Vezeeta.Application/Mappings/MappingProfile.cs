using AutoMapper;
using Vezeeta.Application.Mappings.DTOs;
using Vezeeta.Domain.Entities;

namespace Vezeeta.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, PatientResponseDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.ImagePath) ? src.ImagePath : null));
            CreateMap<RegisterRequestDTO, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.Email}"));
        }

    }
}
