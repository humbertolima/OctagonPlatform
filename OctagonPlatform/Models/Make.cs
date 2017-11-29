using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models
{
    public class Make
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<Model> Models { get; set; }

        public ICollection<Terminal> Terminals { get; set; }    

        public Make()
        {
            Models = new Collection<Model>();
            Terminals = new Collection<Terminal>();
        }

    }
}