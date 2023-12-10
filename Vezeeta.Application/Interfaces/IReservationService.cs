using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Mappings.DTOs;

namespace Vezeeta.Application.Interfaces
{
    public interface IReservationService
    {
        Task<GenericResponse> AddReservatioAsync(string jwtToken, ReservationRequest reservationRequest);
    }
}
