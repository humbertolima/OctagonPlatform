using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OctagonPlatform.Models
{
    public class PermissionSubGroup
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Permission> Permission { get; set; }
        
        [ForeignKey("SetOfPermission")]
        public int SetOfPermissionId { get; set; }

        [JsonIgnore]
        public virtual SetOfPermission SetOfPermission { get; set; }
    }
}