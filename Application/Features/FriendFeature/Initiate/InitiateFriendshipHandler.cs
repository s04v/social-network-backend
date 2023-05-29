using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FriendFeature
{
    public class InitiateFriendshipHandler : IRequestHandler<InitiateFriendshipRequest, Unit>
    {
        private readonly IFriendRepository _friendRepository;
        private readonly IUserRepository _userRepository;

        public InitiateFriendshipHandler(IFriendRepository friendRepository, IUserRepository userRepository)
        {
            _friendRepository = friendRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(InitiateFriendshipRequest request, CancellationToken cancellationToken)
        {
            var requester = _userRepository.GetById(request.RequesterId);
            var receiver = _userRepository.GetById(request.ReceiverId);

            if(requester == null || receiver == null || request.RequesterId == request.ReceiverId)
            {
                throw new BadRequestException("Id is invalid");
            }

            if(_friendRepository.CheckIfRelationExists(requester, receiver))
            {
                throw new BadRequestException("Ralation between users already exists");
            }

            _friendRepository.Initiate(requester, receiver, cancellationToken);

            return Unit.Value;
        }
    }
}
