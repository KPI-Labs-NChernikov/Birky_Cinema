using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string AgeRestriction { get; set; }

        public string Genre { get; set; }

        public string Director { get; set; }

        public string Actors { get; set; }

        public string Authors { get; set; }

        public string Length { get; set; }

        public string Country { get; set; }

        public short Rating { get; set; }

        public string LinkToAffiche { get; set; }

        public string LinkToPoster { get; set; }

        public IEnumerable<SessionReducedViewModel> Sessions { get; set; }

        public IEnumerable<ReviewViewModel> Reviews { get; set; }

        public IEnumerable<MovieCarouselViewModel> Recommended { get; set; }
    }
}
