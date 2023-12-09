using AutoMapper;
using Vezeeta.Application.Mappings.DTOs;
using Vezeeta.Domain.Entities;

namespace Vezeeta.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequestDTO, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.Email}"));

            CreateMap<DoctorRegisterRequestDTO, Doctor>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src));

            CreateMap<User, PatientResponseDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.ImagePath) ? src.ImagePath : null));

            CreateMap<Doctor, DoctorResponseDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.User.BirthDate))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.User.ImagePath))
                .ForMember(dest => dest.SpeciallizationEn, opt => opt.MapFrom(src => src.Specialization.NameEn))
                .ForMember(dest => dest.SpeciallizationAr, opt => opt.MapFrom(src => src.Specialization.NameAr));
        }

    }
}
