using OctagonPlatform.Helpers;

namespace OctagonPlatform.Models
{
    public class User:ISoftDeleted
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsLocked { get; set; }

        
        public bool? Deleted { get; set; }

        //Resto de la Implementacion de User
    }
}