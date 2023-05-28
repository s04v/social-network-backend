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

        public Friend? GetById(int id)
        {
            return _context.Friends.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Friend>? GetInitiationListForUser(int id)
        {
            return _context.Friends.Where(x => x.Receiver.Id == id && x.IsAccepted == false).ToList();
        }

        public IEnumerable<User>? GetFriendListForUser(int id)
        {
            var friends = _context.Friends.Where(x => x.IsAccepted == true && (x.Requester.Id == id || x.Receiver.Id == id)).ToList();
            var friendList = new List<User>();
            foreach (var friend in friends)
            {
                if(friend.Requester.Id == id)
                {
                    friendList.Add(friend.Requester);
                } else
                {
                    friendList.Add(friend.Receiver);
                }
            }

            return friendList;
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

        public void Accept(int id, CancellationToken cancellationToken)
        {
            var initiation = _context.Friends.FirstOrDefault(x => x.Id == id);
            if(initiation != null)
            {
                initiation.IsAccepted = true;
            }
            _context.SaveChangesAsync(cancellationToken);
        }

        public void Delete(int id, CancellationToken cancellationToken)
        {
            var friend = new Friend { Id = id };
            _context.Friends.Remove(friend);
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
