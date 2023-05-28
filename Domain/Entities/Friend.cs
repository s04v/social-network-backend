using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Friend: BaseEntity
    {
        public int Id { get; set; }
        public User Requester { get; set; }
        public User Receiver { get; set; }
        public bool IsAccepted { get; set; }
    }
}
