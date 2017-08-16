using OctagonPlatform.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OctagonPlatform.Models
{
    public class Country:ISoftDeleted
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<State> Stateses { get; set; }

        public Country()
        {
            Stateses = new Collection<State>();
        }

        public bool? Deleted { get; set; }
    }
}