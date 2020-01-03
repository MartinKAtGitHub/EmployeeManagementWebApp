using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.Utilities
{
    // 76  create custom validation attribute
    public class CustomValidEmailAttribute : ValidationAttribute
    {
        private readonly string allowedDomain;

        public CustomValidEmailAttribute(string allowedDomain)
        {
            this.allowedDomain = allowedDomain;
        }
        public override bool IsValid(object value) // This method will automagicly detect the incoming data
        {
            string[] strings = value.ToString().Split('@');
           return strings[1].ToUpper() == allowedDomain.ToUpper();
        }
    }
}
