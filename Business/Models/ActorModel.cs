using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class ActorModel
    {
		public int Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string FirstNameRU { get; set; }

		public string LastNameRU { get; set; }

		public string FirstNameENG { get; set; }

		public string LastNameENG { get; set; }

		public ICollection<int> MovieIds { get; set; }
	}
}
