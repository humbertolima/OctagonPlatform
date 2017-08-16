using OctagonPlatform.Helpers;

namespace OctagonPlatform.Models
{
    public class Logo:ISoftDeleted
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public bool? Deleted { get; set; }
    }
}
