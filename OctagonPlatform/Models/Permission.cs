using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OctagonPlatform.Models
{
    
    public class Permission
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("Parent")]
        public int? ParentID { get; set; }
        public virtual Permission Parent { get; set; }

        public ICollection<User> Users { get; set; }

        public Permission()
        {
            Users = new Collection<User>();
        }
    }
}