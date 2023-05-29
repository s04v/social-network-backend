using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FriendFeature
{
    public class InitiateFriendshipRequest : IRequest<Unit>
    {
        public int RequesterId { get; set; }
        public int ReceiverId { get; set; }
    }
}
