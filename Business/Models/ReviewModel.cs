using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public short Rating { get; set; }


        public string UserId { get; set; }

        public int MovieId { get; set; }
    }
}
