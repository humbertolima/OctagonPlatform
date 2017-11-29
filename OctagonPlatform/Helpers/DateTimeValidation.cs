using System;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Helpers
{
    public class DateTimeValidation: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var d = Convert.ToDateTime(value);
            return d <= DateTime.UtcNow;

        }
    }
}