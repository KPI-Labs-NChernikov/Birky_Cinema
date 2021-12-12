using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class User : IdentityUser
    {
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }


        public ICollection<Review> Reviews { get; set; }

        public ICollection<Genre> Genres { get; set; }

        public ICollection<Movie> Movies { get; set; }

        public ICollection<SessionSeat> SessionSeats { get; set; }
    }
}
