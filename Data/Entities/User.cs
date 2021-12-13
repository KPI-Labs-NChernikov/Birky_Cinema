using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class User : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }


        public ICollection<Review> Reviews { get; set; }

        public ICollection<Genre> Genres { get; set; }

        public ICollection<Movie> Movies { get; set; }

        public ICollection<SessionSeat> SessionSeats { get; set; }
    }
}
