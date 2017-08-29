using OctagonPlatform.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OctagonPlatform.Helpers.CustomValidations
{
    public class ValidPhone: ValidationAttribute
    {
        private readonly ApplicationDbContext _context;

        public ValidPhone()
        {
            _context = new ApplicationDbContext();
        }
        public override bool IsValid(object value)
        {

            var phone = Convert.ToString(value);

            var userPhone = _context.Users.FirstOrDefault(x => x.Phone == phone && !x.Deleted);

            var partnerPhone = _context.Partners.FirstOrDefault(x => x.WorkPhone == phone || x.Mobile == phone && !x.Deleted);

            var partnerContactPhone = _context.PartnerContacts.FirstOrDefault(x => x.Phone == phone && !x.Deleted);

            return (userPhone == null  && partnerPhone == null && partnerContactPhone == null);

        }
    }
}