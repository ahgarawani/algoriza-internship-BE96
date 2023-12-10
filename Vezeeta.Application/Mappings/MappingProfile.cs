using AutoMapper;
using System.Collections.Generic;
using Vezeeta.Application.Mappings.DTOs;
using Vezeeta.Domain.Entities;

namespace Vezeeta.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequest, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.Email}"));

            CreateMap<DoctorRegisterRequest, Doctor>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src));

            CreateMap<User, PatientResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.ImagePath) ? src.ImagePath : null));

            CreateMap<Doctor, DoctorResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.User.BirthDate))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.User.ImagePath))
                .ForMember(dest => dest.SpeciallizationEn, opt => opt.MapFrom(src => src.Specialization.NameEn))
                .ForMember(dest => dest.SpeciallizationAr, opt => opt.MapFrom(src => src.Specialization.NameAr));

            CreateMap<AppointmentsHourDTO, AppointmentsHour>();

            CreateMap<AppointmentsDayDTO, AppointmentsDay>()
                .ForMember(dest => dest.ApointmentsHours, opt => opt.MapFrom(src => src.Hours ));

            CreateMap<AppointmentsRequest, AppointmentsDay>();

            CreateMap<DiscountCodeRequest, DiscountCode>();
        }

    }
}
