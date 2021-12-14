using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class UserViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Genres { get; set; }

        public IEnumerable<MovieCarouselViewModel> Recommended { get; set; }
    }
}
