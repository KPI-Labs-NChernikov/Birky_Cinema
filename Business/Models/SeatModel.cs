using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class SeatModel
    {
        public int Id { get; set; }

        public short PlaceNumber { get; set; }


        public short RowId { get; set; }

        public ICollection<SessionSeatModel> SessionSeatModels { get; set; }
    }
}
