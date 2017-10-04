using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.WebPages;

namespace OctagonPlatform.Helpers
{
    public class StringValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return true;

            if (value.ToString().IsEmpty()) return true;

            var temp = value.ToString();
            const string chars = " AaQqZzWwSsXxEeDdCcRrFfVvTtGgBbYyHhNnUuJjMmIiKk<,OoLl>.Pp:;?/{['}]|+=_-)(*&^%$#@!~`";

            

            return !chars.Any(item => temp.Contains(item.ToString()));
        }
    }
}