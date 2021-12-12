using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class CountryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string NameRU { get; set; }

        public string NameENG { get; set; }

        public ICollection<int> MovieIds { get; set; }
    }
}
