using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
	public class Director
	{
		public int Id { get; set; }

		[Column(TypeName = "nvarchar(50)")]
		public string FirstName { get; set; }

		[Column(TypeName = "nvarchar(50)")]
		public string LastName { get; set; }

		public ICollection<Movie> Movies { get; set; }
	}
}
