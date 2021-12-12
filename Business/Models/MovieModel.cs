using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class MovieModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameRU { get; set; }

        public string NameENG { get; set; }

        public short AgeRestriction { get; set; }

        public string Description { get; set; }

        public string DescriptionRU { get; set; }

        public string DescriptionENG { get; set; }

        public short Length { get; set; }

        public short Rating { get; set; }

        public string LinkToPoster { get; set; }

        public string LinkToAffiche { get; set; }

        public int CountryId { get; set; }


        public ICollection<int> ReviewIds { get; set; }

        public ICollection<int> GenreIds { get; set; }

        public ICollection<int> DirectorIds { get; set; }

        public ICollection<int> ActorIds { get; set; }

        public ICollection<int> ScenarioWriterIds { get; set; }

        public ICollection<string> UserIds { get; set; }

        public ICollection<int> SessionIds { get; set; }
    }
}
