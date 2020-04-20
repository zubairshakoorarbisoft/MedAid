using System.Collections.Generic;

namespace MedAidAPI.Models
{
    public class LocationType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public ICollection<Location> Locations { get; set; }
    }
}
