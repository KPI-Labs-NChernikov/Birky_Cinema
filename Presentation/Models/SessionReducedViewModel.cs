using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class SessionReducedViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<DateTime> Times { get; set; }
    }
}
