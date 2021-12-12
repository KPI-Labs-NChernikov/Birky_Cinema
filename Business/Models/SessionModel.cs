using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class SessionModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public int MovieId { get; set; }

        public ICollection<SessionSeatModel> SessionSeatModels { get; set; }
    }
}
