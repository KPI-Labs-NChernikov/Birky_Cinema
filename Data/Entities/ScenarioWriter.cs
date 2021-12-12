using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
	public class ScenarioWriter
	{
		public int Id { get; set; }

		[Column(TypeName = "nvarchar(50)")]
		public string FirstName { get; set; }

		[Column(TypeName = "nvarchar(50)")]
		public string LastName { get; set; }

		[Column(TypeName = "nvarchar(50)")]
		public string FirstNameRU { get; set; }

		[Column(TypeName = "nvarchar(50)")]
		public string LastNameRU { get; set; }

		[Column(TypeName = "nvarchar(50)")]
		public string FirstNameENG { get; set; }

		[Column(TypeName = "nvarchar(50)")]
		public string LastNameENG { get; set; }

		public ICollection<Movie> Movies { get; set; }
	}
}
