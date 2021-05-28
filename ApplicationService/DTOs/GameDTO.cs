using System;

namespace ApplicationService.DTOs
{
    public class GameDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public DateTime? Release { get; set; }
        public double Price { get; set; }

        public int CategoryId { get; set; }
        public virtual CategoryDTO Category { get; set; }
    }
}
