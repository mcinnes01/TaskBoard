using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskBoard.Core.Data.DTOs
{
    public class UserRecord
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Twitter { get; set; }
        public string Password { get; private set; }
        public bool Alerts { get; set; }
    }
}
