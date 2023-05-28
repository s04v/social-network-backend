using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFriendRepository
    {
        Friend? GetById(int id);
        IEnumerable<Friend>? GetInitiationListForUser(int id); 
        IEnumerable<User>? GetFriendListForUser(int id);
        void Initiate(User requester, User receiver, CancellationToken cancellationToken);
        void Accept(int id, CancellationToken cancellationToken);
        void Delete(int id, CancellationToken cancellationToken);
        bool CheckIfRelationExists(User userA, User userB);
    }
}
