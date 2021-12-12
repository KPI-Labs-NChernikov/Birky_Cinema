using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class SessionSeat
    {
        public int SessionId { get; set; }
        public Session Session { get; set; }

        public int SeatId { get; set; }
        public Seat Seat { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
