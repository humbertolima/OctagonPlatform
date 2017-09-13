using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models
{
    public class LocationType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Terminal> Terminal { get; set; }

        public LocationType()
        {
            Terminal = new Collection<Terminal>();
        }
    }
}