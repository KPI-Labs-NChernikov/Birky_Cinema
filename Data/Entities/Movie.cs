using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Name { get; set; }

        public short AgeRestriction { get; set; }

        public string Description { get; set; }

        public short Length { get; set; }

        public short Rating { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string LinkToPoster { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string LinkToAffiche { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }


        public ICollection<Review> Reviews { get; set; }
    }
}
