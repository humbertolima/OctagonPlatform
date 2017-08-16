using System;
using OctagonPlatform.Helpers;

namespace OctagonPlatform.Models
{
    public class User:IAuditEntity, ISoftDeleted
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsLocked { get; set; }

        
        public bool? Deleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public User CreatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public User DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public User UpdatedBy { get; set; }

        //Resto de la Implementacion de User
    }
}