using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Session
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public ICollection<SessionSeat> SessionSeats { get; set; }
    }
}
