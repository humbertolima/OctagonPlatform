using OctagonPlatform.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OctagonPlatform.Helpers.CustomValidations
{
    public class ValidUserName: ValidationAttribute
    {
        private readonly ApplicationDbContext _context;

        public ValidUserName()
        {
            _context = new ApplicationDbContext();
        }
        public override bool IsValid(object value)
        {
            var userName = Convert.ToString(value).Trim();

            var user = _context.Users.FirstOrDefault(x => x.UserName == userName && !x.Deleted);

            return (user == null) ? true : false;

        }

    }
}