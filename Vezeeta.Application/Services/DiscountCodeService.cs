﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Interfaces;
using Vezeeta.Application.Mappings.DTOs;
using Vezeeta.Domain;
using Vezeeta.Domain.Entities;

namespace Vezeeta.Application.Services
{
    public class DiscountCodeService: IDiscountCodeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DiscountCodeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GenericResponse> AddCodeAsync(DiscountCodeRequest discountCodeRequest)
        {
            var discountCode = _mapper.Map<DiscountCode>(discountCodeRequest);
            try
            {
                await _unitOfWork.DiscountCodes.AddCodeAsync(discountCode);
                _unitOfWork.Complete();
                return new GenericResponse { Succeeded = true, Message = "Discount Code Added Successfully!" };
            }
            catch
            {
                return new GenericResponse { Succeeded = false, Message = "An error occurred! Likely a duplicate code was sent!" };
            }
        }

        public async Task<GenericResponse> UpdateCodeAsync(DiscountCodeRequest discountCodeRequest)
        {
            var discountCode = await _unitOfWork.DiscountCodes.GetByCodeAsync(discountCodeRequest.Code);
            if (discountCode == null)
                return new GenericResponse { Succeeded = false, Message = "An error occurred! Likely there was no such Id in the database!" };
            discountCode.Code = discountCodeRequest.Code;
            discountCode.RemainingUsage = discountCodeRequest.RemainingUsage;
            discountCode.Value = discountCodeRequest.Value;
            discountCode.Type = discountCodeRequest.Type;
            discountCode.IsActive = discountCodeRequest.IsActive;

            try
            {
                _unitOfWork.DiscountCodes.UpdateCode(discountCode);
                _unitOfWork.Complete();
                return new GenericResponse { Succeeded = true, Message = "Discount Code Updated Successfully!" };
            }
            catch
            {
                return new GenericResponse { Succeeded = false, Message = "An error occurred! Likely a duplicate code was sent!" };
            }
        }

        public async Task<GenericResponse> DeleteCodeAsync(int codeId)
        {
            var discountCode = await _unitOfWork.DiscountCodes.GetByIdAsync(codeId);
            if (discountCode == null)
                return new GenericResponse { Succeeded = false, Message = "An error occurred! Likely there was no such Id in the database!" };
            
            try
            {
                _unitOfWork.DiscountCodes.DeactivateCode(discountCode);
                _unitOfWork.Complete();
                return new GenericResponse { Succeeded = true, Message = "Discount Code Deleted Successfully!" };
            }
            catch
            {
                return new GenericResponse { Succeeded = false, Message = "An error occurred! Likely a duplicate code was sent!" };
            }
        }

        public async Task<GenericResponse> DeactivateCodeAsync(int codeId)
        {
            var discountCode = await _unitOfWork.DiscountCodes.GetByIdAsync(codeId);
            if (discountCode == null)
                return new GenericResponse { Succeeded = false, Message = "An error occurred! Likely there was no such Id in the database!" };

            try
            {
                _unitOfWork.DiscountCodes.DeactivateCode(discountCode);
                _unitOfWork.Complete();
                return new GenericResponse { Succeeded = true, Message = "Discount Code Deactivated Successfully!" };
            }
            catch
            {
                return new GenericResponse { Succeeded = false, Message = "An error occurred! Likely a duplicate code was sent!" };
            }
        }
    }
}
