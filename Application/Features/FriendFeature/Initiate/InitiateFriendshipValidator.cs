using Application.Features.UserFeature.CreateUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FriendFeature
{
    public class InitiateFriendshipValidator : AbstractValidator<InitiateFriendshipRequest>
    {
        public InitiateFriendshipValidator()
        {
            RuleFor(x => x.RequesterId).NotEmpty();
            RuleFor(x => x.ReceiverId).NotEmpty();
        }
    }
}
