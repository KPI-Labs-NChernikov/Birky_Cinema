using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class MainPageViewModel
    {
        public MovieReducedViewModel[] Posters { get; set; }

        public IEnumerable<MovieCarouselViewModel> ComingSoon { get; set; }

        public IEnumerable<MovieCarouselViewModel> Shorts { get; set; }
    }
}
