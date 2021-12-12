using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class RowModel
    {
        public short Id { get; set; }

        public decimal Price { get; set; }

        public ICollection<int> SeatIds { get; set; }
    }
}
