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
using BCryptNet = BCrypt.Net.BCrypt;

namespace Application.Features.UserFeature.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            if (_userRepository.CheckIfUserExists(request.Email))
            {
                throw new UserAlreadyExistsException();
            }

            string passwordHash = BCryptNet.HashPassword(request.Password, workFactor: 10);

            var user = _mapper.Map<User>(request);
            user.Password = passwordHash;
            _userRepository.Create(user, cancellationToken);

            return Unit.Value;
        }
    }
}
