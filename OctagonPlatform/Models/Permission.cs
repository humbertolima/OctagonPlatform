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

        [Required]
        public string Type { get; set; }

        [Required]
        [ForeignKey("SetOfPermission")]
        public int SetOfPermissionId { get; set; }

        public virtual SetOfPermission SetOfPermission { get; set; }

        public ICollection<User> Users { get; set; }

        public Permission()
        {
            Users = new Collection<User>();
        }
    }
}