using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Review
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(400)")]
        public string Text { get; set; }

        public short Rating { get; set; }


        public string UserId { get; set; }
        public User User { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
