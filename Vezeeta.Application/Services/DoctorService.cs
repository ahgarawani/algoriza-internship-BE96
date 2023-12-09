using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Interfaces;
using Vezeeta.Application.Mappings.DTOs;
using Vezeeta.Domain.Entities;
using Vezeeta.Domain;
using Vezeeta.Domain.Enums;

namespace Vezeeta.Application.Services
{
    public class DoctorService: IDoctorService
    {
        readonly private IUnitOfWork _unitOfWork;
        readonly private IMapper _mapper;

        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DoctorResponseDTO>> GetAllAsync(UserPaginatedSearchQueryDTO queries)
        {
            var doctors = await _unitOfWork.Doctors.GetAllAsync();
            var filteredDoctors = doctors
                .Where(doctor =>
                    doctor.User.FirstName.IndexOf(queries.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    doctor.User.LastName.IndexOf(queries.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    doctor.User.Gender.ToString().ToLower().Contains(queries.Search.ToLower()) ||
                    doctor.User.Email.IndexOf(queries.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    doctor.User.PhoneNumber.IndexOf(queries.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    doctor.Specialization.NameEn.IndexOf(queries.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    doctor.Specialization.NameAr.IndexOf(queries.Search, StringComparison.OrdinalIgnoreCase) >= 0 
                )
                .Skip((queries.Page - 1) * queries.PageSize)
                .Take(queries.PageSize)
                .ToList();
            return _mapper.Map<List<DoctorResponseDTO>>(filteredDoctors);
        }

        public async Task<DoctorResponseDTO> GetByIdAsync(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
            return _mapper.Map<DoctorResponseDTO>(doctor);
        }

        public async Task<(bool Succeeded, string Message)> AddAsync(DoctorRegisterRequestDTO doctorRegisterRequest)
        {
            if (await _unitOfWork.Users.FindByEmailAsync(doctorRegisterRequest.Email) is not null)
                return (false, "Email is already registered!");

            if (!await _unitOfWork.Specializations.DoesExist(doctorRegisterRequest.SpecializationId))
                return (false, "Invalid Specialization Id!");

            var doctor = _mapper.Map<Doctor>(doctorRegisterRequest);
            var result = await _unitOfWork.Doctors.AddAsync(doctor, doctorRegisterRequest.Password);

            if (!result.Succeeded) return (false, result.errorsMessage);

            doctor.User.ImagePath = ImageStorageService.SaveImage(doctorRegisterRequest.Image, doctor.User.Id);
            
            _unitOfWork.Complete();

            return (true, "Doctor Registered Successfully!");
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(Id);
            if (doctor == null) 
                return false;
            else
            {
                ImageStorageService.DeleteImage(doctor.User.ImagePath);
                await _unitOfWork.Doctors.DeleteAsync(Id); 
                return true;
            }
        }
        public async Task<(bool Succeeded, string Message)> EditAsync(int Id, DoctorEditRequestDTO doctorEditRequest)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(Id);

            if (doctor == null)
                return (false, $"No Doctors With ID: {Id}!");
            doctor.SpecializationId = doctorEditRequest.SpecializationId != default(int)? doctorEditRequest.SpecializationId: doctor.SpecializationId;
            doctor.VisitPrice = doctorEditRequest.VisitPrice != default(float) ? doctorEditRequest.VisitPrice : doctor.VisitPrice;
            doctor.User.FirstName = doctorEditRequest.FirstName != null ? doctorEditRequest.FirstName : doctor.User.FirstName;
            doctor.User.LastName = doctorEditRequest.LastName != null ? doctorEditRequest.LastName : doctor.User.LastName;
            doctor.User.Email = doctorEditRequest.Email != null ? doctorEditRequest.Email : doctor.User.Email;
            doctor.User.PhoneNumber = doctorEditRequest.PhoneNumber != null ? doctorEditRequest.PhoneNumber : doctor.User.PhoneNumber;
            doctor.User.Gender = doctorEditRequest.Gender != default(Gender) ? doctorEditRequest.Gender : doctor.User.Gender;
            doctor.User.BirthDate = doctorEditRequest.BirthDate != default(DateTime) ? doctorEditRequest.BirthDate : doctor.User.BirthDate;

            if (doctorEditRequest.Image != null)
            {
                ImageStorageService.DeleteImage(doctor.User.ImagePath);
                doctor.User.ImagePath = ImageStorageService.SaveImage(doctorEditRequest.Image, doctor.User.Id);
            }

            
            _unitOfWork.Doctors.Edit(doctor);
            _unitOfWork.Complete();

            return (true, "Doctor Updated Successfully!");
        }

        public async Task<(bool Succeeded, string Message)> ChangeVisitPrice(int Id, float newPrice)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(Id);

            if (doctor == null)
                return (false, $"No Doctors With ID: {Id}!");

            doctor.VisitPrice = newPrice;

            _unitOfWork.Doctors.Edit(doctor);
            _unitOfWork.Complete();

            return (true, "Visit Price Updated Successfully!");
        }
    }
}
