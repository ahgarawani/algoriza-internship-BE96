using AutoMapper;
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
                .ForMember(dest => dest.ApointmentsHours, opt => opt.MapFrom(src => src.Hours));

            CreateMap<AppointmentsRequest, AppointmentsDay>();

            CreateMap<DiscountCodeRequest, DiscountCode>();

            CreateMap<ReservationRequest, Reservation>()
                .ForPath(dest => dest.DiscountCodeUser.DiscountCode.Code, opt => opt.MapFrom(src => src.DiscountCode));

            CreateMap<Reservation, ReservationDoctorResponse>()
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTime.Now.Year - src.User.BirthDate.Year))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.User.ImagePath))
                .ForMember(dest => dest.Appointment, opt => opt.MapFrom(src => $"{src.AppointmentsHour.AppointmentsDay.Day} {src.AppointmentsHour.Hour}"));

            CreateMap<Reservation, ReservationPatientResponse>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.AppointmentsHour.AppointmentsDay.Doctor.User.ImagePath))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => $"{src.AppointmentsHour.AppointmentsDay.Doctor.User.FirstName} {src.AppointmentsHour.AppointmentsDay.Doctor.User.LastName}"))
                .ForMember(dest => dest.SpecializationAr, opt => opt.MapFrom(src => src.AppointmentsHour.AppointmentsDay.Doctor.Specialization.NameAr))
                .ForMember(dest => dest.SpecializationEn, opt => opt.MapFrom(src => src.AppointmentsHour.AppointmentsDay.Doctor.Specialization.NameEn))
                .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.AppointmentsHour.AppointmentsDay.Day))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.AppointmentsHour.Hour))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.AppointmentsHour.AppointmentsDay.Doctor.VisitPrice))
                .ForMember(dest => dest.DiscountCode, opt => opt.MapFrom(src => src.DiscountCodeUser.DiscountCode.Code))
                .ForMember(dest => dest.FinalPrice, opt => opt.MapFrom(src => src.FinalPrice))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }

    }
}
