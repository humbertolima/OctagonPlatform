using OctagonPlatform.Helpers;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models
{
    public class Logo:ISoftDeleted
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Logo's name is required")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The logo is required")]
        [Display(Name = "Logo")]
        public string Picture { get; set; }

        public bool Deleted { get; set; }
    }
}
