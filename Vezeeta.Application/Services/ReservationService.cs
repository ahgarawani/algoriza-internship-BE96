using AutoMapper;
using Vezeeta.Application.Interfaces;
using Vezeeta.Application.Mappings.DTOs;
using Vezeeta.Domain;
using Vezeeta.Domain.Entities;
using Vezeeta.Domain.Enums;

namespace Vezeeta.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ReservationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<GenericResponse> AddAsync(int userId, ReservationRequest reservationRequest)
        {
            var appointmentsHour = await _unitOfWork.Appointments.GetAppointmentsHourByIdAsync(reservationRequest.AppointmentsHourId);
            if (appointmentsHour == null)
                return new GenericResponse { Succeeded = false, Message = "Reservation failed! Invalid appointment!" };

            var reservation = new Reservation()
            {
                UserId = userId,
                AppointmentsHourId = reservationRequest.AppointmentsHourId,
                FinalPrice = appointmentsHour.AppointmentsDay.Doctor.VisitPrice,
            };

            if (reservationRequest.DiscountCode != null)
            {
                var discountCode = await _unitOfWork.DiscountCodes.GetByCodeAsync(reservationRequest.DiscountCode);

                if (reservationRequest.DiscountCode == null)
                    return new GenericResponse { Succeeded = false, Message = "Invalid Discount Code!" };

                discountCode.RemainingUsage -= 1;
                if (discountCode.RemainingUsage == 0) discountCode.IsActive = false;
                var discountCodeUser = new DiscountCodeUser
                {
                    DiscountCode = discountCode,
                    UserId = userId,
                };
                reservation.DiscountCodeUser = discountCodeUser;
                reservation.FinalPrice = discountCode.Type == DiscountType.Percentage ?
                    reservation.FinalPrice * discountCode.Value : reservation.FinalPrice - discountCode.Value;
            }


            try
            {
                await _unitOfWork.Reservations.AddAsync(reservation);
                _unitOfWork.Complete();
                return new GenericResponse { Succeeded = true, Message = "Reservation made successfully!" };
            }
            catch
            {
                return new GenericResponse { Succeeded = false, Message = "An error occurred!" };
            }
        }

        public async Task<int> CountAsync()
        {
            return await _unitOfWork.Reservations.CountAsync();
        }

        public async Task<IEnumerable<ReservationDoctorResponse>> GetAllByDoctorAsync(int userId, UserPaginatedSearchQuery queries)
        {

            var doctorId = (await _unitOfWork.Doctors.GetByUserIdAsync(userId)).Id;
            var reservations = await _unitOfWork.Reservations.GetAllByDoctorAsync(doctorId);
            var filteredReservations = reservations.Where(reservation =>
                    reservation.AppointmentsHour.Hour.IndexOf(queries.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    reservation.AppointmentsHour.AppointmentsDay.Day.ToString().IndexOf(queries.Search, StringComparison.OrdinalIgnoreCase) >= 0
                )
                .Skip((queries.Page - 1) * queries.PageSize)
                .Take(queries.PageSize)
                .ToList();
            return _mapper.Map<List<ReservationDoctorResponse>>(reservations);
        }

        public async Task<IEnumerable<ReservationPatientResponse>> GetAllByPatientAsync(int userId)
        {

            var reservations = (await _unitOfWork.Reservations.GetAllByPatientAsync(userId)).ToList();
            return _mapper.Map<List<ReservationPatientResponse>>(reservations);
        }

        public async Task<(int Status, string Message)> PatientCancellation(int userId, int reservationId)
        {

            if (!await _unitOfWork.Reservations.AuthenticatePatientAsync(reservationId, userId))
                return (403, "Access Denied!");
            try
            {
                _unitOfWork.Reservations.ChangeStatus(reservationId, Status.Cancelled);
                return (200, "Reservation Cancelled Successfully");
            }
            catch
            {
                return (400, "An error occureed!");
            }
        }

        public async Task<(int Status, string Message)> DoctorConfirmation(int userId, int reservationId)
        {

            var doctorId = (await _unitOfWork.Doctors.GetByUserIdAsync(userId)).Id;


            if (!await _unitOfWork.Reservations.AuthenticateDoctorAsync(reservationId, doctorId))
                return (403, "Access Denied!");
            try
            {
                _unitOfWork.Reservations.ChangeStatus(reservationId, Status.Confirmed);
                return (200, "Reservation Confirmed Successfully");
            }
            catch
            {
                return (400, "An error occureed!");
            }
        }
    }
}
