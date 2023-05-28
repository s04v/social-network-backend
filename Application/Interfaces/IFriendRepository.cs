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
        IEnumerable<User>? GetList();
        IEnumerable<Friend>? GetInitiationList(); 
        IEnumerable<User>? GetListById(int id);
        void Initiate(User requester, User receiver, CancellationToken cancellationToken);
        void ChangeStatus(bool isAccepted);
        void Delete(int id);
        bool CheckIfRelationExists(User userA, User userB);
    }
}
