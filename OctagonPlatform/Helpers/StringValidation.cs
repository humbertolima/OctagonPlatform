using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Helpers
{
    public class StringValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            const string chars = " /~!@#$%^&*()+=?><:;}]{[<,>.`";
            var result = true;
            foreach (var item in chars)
            {
                if (value.ToString().Contains(item.ToString()))
                    result = false;
            }

            return result;

        }
    }
}