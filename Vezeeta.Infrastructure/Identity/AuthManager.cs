using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using Vezeeta.Application.Services;
using Vezeeta.Domain.Entities;

namespace Vezeeta.Infrastructure.Identity
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<User> _userManager;

        private readonly IMapper _mapper;
        private readonly JWT _jwt;

        public AuthManager(UserManager<User> userManager, IMapper mapper, IOptions<JWT> jwt)
        {
            _userManager = userManager;

            _mapper = mapper;
            _jwt = jwt.Value;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest registerRequest)
        {
            if (await _userManager.FindByEmailAsync(registerRequest.Email) is not null)
                return new AuthResponse { Message = "Email is already registered!" };

            var user = _mapper.Map<User>(registerRequest);
            var path = "";
            if (registerRequest.Image != null)
            {
                var rootDirpath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "ImageStorage");
                Directory.CreateDirectory(rootDirpath);
                var filename = $@"Image-User{user.Id}-{DateTime.Now.Ticks}{Path.GetExtension(registerRequest.Image.FileName)}";
                path = Path.Combine(rootDirpath, filename);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    registerRequest.Image.CopyTo(stream);
                }
            }
            user.ImagePath = path;

            var result = await _userManager.CreateAsync(user, registerRequest.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description} ";

                return new AuthResponse { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, "Patient");

            var jwtSecurityToken = await CreateJwt(user);

            return new AuthResponse
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest loginRequest)
        {
            var authModel = new AuthResponse();

            var user = await _userManager.FindByEmailAsync(loginRequest.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwt(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;

            return authModel;
        }

        private async Task<JwtSecurityToken> CreateJwt(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));


            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("int", user.Id.ToString())
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
