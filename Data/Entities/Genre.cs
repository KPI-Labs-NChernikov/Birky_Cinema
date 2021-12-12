using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Genre
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string NameRU { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string NameENG { get; set; }

        public ICollection<Movie> Movies { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
