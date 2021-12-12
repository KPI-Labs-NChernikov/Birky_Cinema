using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Seat
    {
        public int Id { get; set; }

        public short PlaceNumber { get; set; }


        public short RowId { get; set; }
        public Row Row { get; set; }
    }
}
