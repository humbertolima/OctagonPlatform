﻿using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class UserLoginViewModel
    {
        public int Id { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User name is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int TriesToLogin { get; set; }

        public bool IsLocked { get; set; }

        public Partner  Partner { get; set; }

        public string BusinessName { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}