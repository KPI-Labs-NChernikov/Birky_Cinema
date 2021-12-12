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

        [Column(TypeName = "nvarchar(150)")]
        public string NameRU { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string NameENG { get; set; }

        public short AgeRestriction { get; set; }

        public string Description { get; set; }

        public string DescriptionRU { get; set; }

        public string DescriptionENG { get; set; }

        public short Length { get; set; }

        public short Rating { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string LinkToPoster { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string LinkToAffiche { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }


        public ICollection<Review> Reviews { get; set; }

        public ICollection<Genre> Genres { get; set; }

        public ICollection<Director> Directors { get; set; }

        public ICollection<Actor> Actors { get; set; }

        public ICollection<ScenarioWriter> ScenarioWriters { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<Session> Sessions { get; set; }
    }
}
