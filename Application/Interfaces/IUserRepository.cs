using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        User Get(int id);
        IEnumerable<User> GetAll();
        void Create(User user, CancellationToken cancellationToken);
        void Update(User user, CancellationToken cancellationToken);
        bool CheckIfUserExists(string email);
        User GetUserById(int id);
    }
}
