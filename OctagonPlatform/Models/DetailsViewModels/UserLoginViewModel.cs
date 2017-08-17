using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models.DetailsViewModels
{
    public class UserLoginViewModel
    {
        [Display(Name = "")]
        [Required(ErrorMessage = "User name is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int TriesToLogin { get; set; }
  
    }
}