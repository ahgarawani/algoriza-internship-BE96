using AutoMapper;
using Vezeeta.Application.Interfaces;
using Vezeeta.Application.Mappings.DTOs;
using Vezeeta.Domain;
using Vezeeta.Domain.Entities;

namespace Vezeeta.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        readonly private IMapper _mapper;
        readonly private IUnitOfWork _unitOfWork;
        readonly private IJwtGeneratorService _jwtGeneratorService;


        public AuthenticationService(IMapper mapper, IUnitOfWork unitOfWork, IJwtGeneratorService jwtGeneratorService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _jwtGeneratorService = jwtGeneratorService;
        }

        public async Task<AuthenticationResponseDTO> RegisterAsync(RegisterRequestDTO registerRequest)
        {
            if (await _unitOfWork.Users.FindByEmailAsync(registerRequest.Email) is not null)
                return new AuthenticationResponseDTO { Message = "Email is already registered!" };

            var user = _mapper.Map<User>(registerRequest);
            var result = await _unitOfWork.Patients.AddAsync(user, registerRequest.Password);

            if (!result.Succeeded) return new AuthenticationResponseDTO { Message = result.errorsMessage };

            user.ImagePath = ImageStorageService.SaveImage(registerRequest.Image, user.Id);
            _unitOfWork.Complete();

            var jwtSecurityToken = await _jwtGeneratorService.CreateJwt(user);

            return new AuthenticationResponseDTO
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ExpiresOn,
                IsAuthenticated = true,
                Token = jwtSecurityToken.Token,
            };
        }


        public async Task<AuthenticationResponseDTO> LoginAsync(LoginRequestDTO loginRequest)
        {
            var authResponse = new AuthenticationResponseDTO();

            var user = await _unitOfWork.Users.MatchEmailAndPasswordAsync(loginRequest.Email, loginRequest.Password);

            if (user is null)
            {
                authResponse.Message = "Email or Password is incorrect!";
                return authResponse;
            }

            var jwtSecurityToken = await _jwtGeneratorService.CreateJwt(user);

            authResponse.IsAuthenticated = true;
            authResponse.Token = jwtSecurityToken.Token;
            authResponse.Email = user.Email;
            authResponse.ExpiresOn = jwtSecurityToken.ExpiresOn;

            return authResponse;
        }
    }
}
