using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Application.Features.UserFeature.LoginUser
{
    public class LoginUserHandler : IRequestHandler<LoginUserRequest, LoginUserResponse>
    {
        private readonly IUserRepository _userRepository;

        public LoginUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var candidate = _userRepository.GetUserByEmail(request.Email);
            if (candidate == null)
            {
                throw new InvalidCredentialsException();
            }

            bool passwordVerify = BCryptNet.Verify(request.Password, candidate.Password);
            if (!passwordVerify)
            {
                throw new InvalidCredentialsException();
            }

            var token = GenerateJWT(candidate);
            var response = new LoginUserResponse();
            response.Token = token;

            return response;
        }

        public string GenerateJWT(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
            };

            const string KEY = "vbXSZTqV2+K1vckHeVmm/W8n8W4GzTwr6nQHWKhtROCwBLkwxd3Rpig0o8i3Pyr0";

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");

            var jwt = new JwtSecurityToken(
                notBefore: DateTime.UtcNow,
                claims: claimsIdentity.Claims,
                expires: DateTime.UtcNow.AddHours(10),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY)), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
