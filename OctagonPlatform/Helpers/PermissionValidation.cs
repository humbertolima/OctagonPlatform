using System;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Helpers
{
    //poner no heredable segun msdn. https://msdn.microsoft.com/en-us/library/cc668224.aspx

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
    sealed public class PermissionValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;

           
            return false;
        }
    }
}