using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int SetOfPermissionId { get; set; }

        public ICollection<User> Users { get; set; }

        public Permission()
        {
            Users = new Collection<User>();
        }
    }
}