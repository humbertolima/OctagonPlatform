﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models
{
    public class Country
    {
        //prueba5
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The country's name is required")]
        [StringLength(20)]
        public string Name { get; set; }

        public ICollection<State> Stateses { get; set; }
        
        public Country()
        {
            Stateses = new Collection<State>();
        }


    }
}