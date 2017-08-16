using OctagonPlatform.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OctagonPlatform.Models
{
    public class State:ISoftDeleted
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public Country  Country { get; set; }

        public ICollection<City> Cities { get; set; }

        public State()
        {
            Cities = new Collection<City>();
        }

        public bool? Deleted { get; set; }
    }
}