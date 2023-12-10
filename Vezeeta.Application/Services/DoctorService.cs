using AutoMapper;
using Vezeeta.Application.Interfaces;
using Vezeeta.Application.Mappings.DTOs;
using Vezeeta.Domain;
using Vezeeta.Domain.Entities;
using Vezeeta.Domain.Enums;

namespace Vezeeta.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DoctorResponse>> GetAllAsync(UserPaginatedSearchQuery queries)
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
            return _mapper.Map<List<DoctorResponse>>(filteredDoctors);
        }

        public async Task<DoctorResponse> GetByIdAsync(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
            return _mapper.Map<DoctorResponse>(doctor);
        }

        public async Task<GenericResponse> AddAsync(DoctorRegisterRequest doctorRegisterRequest)
        {
            if (await _unitOfWork.Users.FindByEmailAsync(doctorRegisterRequest.Email) is not null)
                return new GenericResponse { Succeeded = false, Message = "Email is already registered!" };

            if (!await _unitOfWork.Specializations.DoesExist(doctorRegisterRequest.SpecializationId))
                return new GenericResponse { Succeeded = false, Message = "Invalid Specialization Id!" };

            var doctor = _mapper.Map<Doctor>(doctorRegisterRequest);
            var result = await _unitOfWork.Doctors.AddAsync(doctor, doctorRegisterRequest.Password);

            if (!result.Succeeded) 
                return new GenericResponse { Succeeded = false, Message = result.errorsMessage };

            doctor.User.ImagePath = ImageStorageService.SaveImage(doctorRegisterRequest.Image, doctor.User.Id);

            _unitOfWork.Complete();

            return new GenericResponse { Succeeded = true, Message = "Doctor Registered Successfully!" };
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
        public async Task<GenericResponse> EditAsync(int Id, DoctorEditRequest doctorEditRequest)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(Id);

            if (doctor == null)
                return new GenericResponse { Succeeded = false, Message = $"No Doctors With ID: {Id}!" };
            doctor.SpecializationId = doctorEditRequest.SpecializationId != default(int) ? doctorEditRequest.SpecializationId : doctor.SpecializationId;
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

            return new GenericResponse { Succeeded = true, Message = "Doctor Updated Successfully!" };
        }

        public async Task<GenericResponse> ChangeVisitPrice(int Id, float newPrice)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(Id);

            if (doctor == null)
                return new GenericResponse { Succeeded = false, Message = $"No Doctors With ID: {Id}!" };

            doctor.VisitPrice = newPrice;

            _unitOfWork.Doctors.Edit(doctor);
            _unitOfWork.Complete();

            return new GenericResponse { Succeeded = true, Message = "Visit Price Updated Successfully!" };
        }
    }
}
