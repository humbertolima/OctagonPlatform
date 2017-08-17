using OctagonPlatform.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models
{
    public class State:ISoftDeleted
    {
        //Prueba 3
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is required")]
        public string Name { get; set; }

        [Display(Name = "Country")]
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