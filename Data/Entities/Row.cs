using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Row
    {
        public short Id { get; set; }

        public decimal Price { get; set; }

        public ICollection<Seat> Seats { get; set; }
    }
}
