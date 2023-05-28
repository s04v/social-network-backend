using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private readonly DataContext _context;

        public FriendRepository(DataContext context)
        {
            _context = context;
        }

        public void ChangeStatus(bool isAccepted)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Friend>? GetInitiationList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User>? GetList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User>? GetListById(int id)
        {
            throw new NotImplementedException();
        }

        public void Initiate(User requester, User receiver, CancellationToken cancellationToken)
        {
            var friend = new Friend
            {
                Requester = requester,
                Receiver = receiver,
                IsAccepted = false,
            };

            _context.Add(friend);
            _context.SaveChangesAsync(cancellationToken);
        }
        
        public bool CheckIfRelationExists(User userA, User userB)
        {
            var friend = _context.Friends.Where(x => 
                    (x.Requester == userA && x.Receiver == userB) ||
                    (x.Requester == userB && x.Receiver == userA))
                .FirstOrDefault();

            if(friend == null)
            {
                return false;
            }

            return true;
        }

    }
}
