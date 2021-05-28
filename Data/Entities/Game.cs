using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Game : BaseEntity
    {
        public string Name { get; set; }

        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public DateTime? Release { get; set; }
        public double Price { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
