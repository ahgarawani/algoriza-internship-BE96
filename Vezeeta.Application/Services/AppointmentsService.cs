using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Interfaces;
using Vezeeta.Application.Mappings.DTOs;
using Vezeeta.Domain;
using Vezeeta.Domain.Entities;
using Vezeeta.Domain.Enums;

namespace Vezeeta.Application.Services
{
    public class AppointmentsService: IAppointmentsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtParserService _jwtParserService;
        private readonly IMapper _mapper;

        public AppointmentsService(IUnitOfWork unitOfWork, IJwtParserService jwtParserService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jwtParserService = jwtParserService;
            _mapper = mapper;
        }

        public async Task<GenericResponse> AddAppointmentsAsync(string jwtToken, AppointmentsRequest appointmentsRequest)
        {
            var userId = _jwtParserService.ParseJwt(jwtToken).UserId;
            var doctor = await _unitOfWork.Doctors.GetByUserIdAsync(userId);
            var appointmentsDays = _mapper.Map<List<AppointmentsDay>>(appointmentsRequest.Days);

            try
            {
                await _unitOfWork.Appointments.AddAppointmentsAsync(doctor, appointmentsDays);
                _unitOfWork.Complete();
                return new GenericResponse { Succeeded = true, Message="Appointments Added Successfully!" };
            }
            catch
            {
                return new GenericResponse { Succeeded = false, Message = "An error occurred! Likely the request contained duplicate appointments."};
            }
        }

        public async Task<(int Status, string Message)> UpdateAppointmentAsync(string jwtToken, int appointmentId, AppointmentsHourDTO appointmentsHour)
        {

            //Check if the given appointment is in the database.
            var appointment = await _unitOfWork.Appointments.GetAppointmentsHourByIdAsync(appointmentId);
            if (appointment == null)
                return (204, "No such appointment was found!");

            //Check if the given appointment record belongs to the logged in user.
            var userId = _jwtParserService.ParseJwt(jwtToken).UserId;
            var doctor = await _unitOfWork.Doctors.GetByUserIdAsync(userId);
            if (doctor.Id != appointment.AppointmentsDay.DoctorId)
                return (403, "Access Denied!");

            //Check for active Reservations.
            var activeReservations = appointment.Reservations.Where(res => res.Status == Status.Pending || res.Status == Status.Confirmed);
            if (activeReservations.Any())
                return (400, "Appointment has active reservations!");

            //Try to make the update
            appointment.Hour = appointmentsHour.Hour;
            try
            {
                _unitOfWork.Appointments.UpdateHour(appointment);
                _unitOfWork.Complete();
                return (200, "Appointment Upadted Succeessfully!");
            }
            catch
            {
                return (400, "An error occurred! Likely a duplicate appointment was sent!");
            }
            
        }

        public async Task<(int Status, string Message)> DeleteAppointmentAsync(string jwtToken, int appointmentId)
        {
            //Check if the given appointment is in the database.
            var appointment = await _unitOfWork.Appointments.GetAppointmentsHourByIdAsync(appointmentId);
            if (appointment == null)
                return (204, "No such appointment was found!");

            //Check if the given appointment record belongs to the logged in user.
            var userId = _jwtParserService.ParseJwt(jwtToken).UserId;
            var doctor = await _unitOfWork.Doctors.GetByUserIdAsync(userId);
            if (doctor.Id != appointment.AppointmentsDay.DoctorId)
                return (403, "Access Denied!");

            //Check for active Reservations.
            var activeReservations = appointment.Reservations.Where(res => res.Status == Status.Pending || res.Status == Status.Confirmed);
            if (activeReservations.Any())
                return (400, "Appointment has active reservations!");

            //Try to delete
            try
            {
                _unitOfWork.Appointments.DeleteHour(appointment);
                _unitOfWork.Complete();
                return (200, "Appointment Deleted Succeessfully!");
            }
            catch
            {
                return (400, "An error occurred! Likely a duplicate appointment was sent!");
            }
        }
    }
}
