using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
