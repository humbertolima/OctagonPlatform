using OctagonPlatform.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OctagonPlatform.Helpers.CustomValidations
{
    public class ValidBusinessName: ValidationAttribute
    {
        private readonly ApplicationDbContext _context;

        public ValidBusinessName()
        {
            _context = new ApplicationDbContext();
        }
        public override bool IsValid(object value)
        {
            var businessName = Convert.ToString(value).Trim();

            var user = _context.Partners.FirstOrDefault(x => x.BusinessName == businessName && !x.Deleted);

            return (user == null);

        }
    }
}