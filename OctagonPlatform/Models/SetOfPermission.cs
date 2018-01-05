using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models
{
    public class SetOfPermission
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Permission> Permissions { get; set; }

        public ICollection<PermissionSubGroup> PermissionSubGroups { get; set; }

        public SetOfPermission()
        {
            Permissions = new Collection<Permission>();
            PermissionSubGroups = new Collection<PermissionSubGroup>();
        }
    }
}