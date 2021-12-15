using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class ReviewViewModel
    {
        public int Id { get; set; }

        public string AuthorName { get; set; }

        public short Rate { get; set; }

        public string Text { get; set; }

        public bool CanUserChange { get; set; }
    }
}
