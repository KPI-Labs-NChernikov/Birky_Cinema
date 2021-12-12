using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Row
    {
        public short Id { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public ICollection<Seat> Seats { get; set; }
    }
}
