using OctagonPlatform.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OctagonPlatform.Helpers.CustomValidations
{
    public class ValidEmail: ValidationAttribute
    {
        private readonly ApplicationDbContext _context;

        public ValidEmail()
        {
            _context = new ApplicationDbContext();
        }
        public override bool IsValid(object value)
        {
            var email = Convert.ToString(value).Trim();

            var user = _context.Users.FirstOrDefault(x => x.Email == email && !x.Deleted);

            return (user == null);

        }
    }
}