using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(User user, CancellationToken cancellationToken)
        {
            _context.Add(user);
            _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(User user, CancellationToken cancellationToken)
        {
            _context.Update(user);
            _context.SaveChangesAsync(cancellationToken);
        }

        public bool CheckIfUserExist(User user)
        {
            return _context.Users.FirstOrDefault(u => u.Email == user.Email) != null ? true : false;
        }

        public User? GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public bool CheckIfUserExists(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email) != null ? true : false;
        }
        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}
