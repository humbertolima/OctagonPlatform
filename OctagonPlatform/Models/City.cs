using OctagonPlatform.Helpers;

namespace OctagonPlatform.Models
{
    public class City:ISoftDeleted
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int StateId { get; set; }

        public State State { get; set; }

        public bool? Deleted { get; set; }
    }
}