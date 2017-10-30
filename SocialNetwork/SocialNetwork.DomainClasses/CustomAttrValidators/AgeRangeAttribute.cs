using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Client2.DomainClasses.CustomAttrValidators
{
    public class AgeRangeAttribute :ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value==null)
            {
                return true;
            }

            var age = (int)value;

            if (1<=age && age<=120)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

    }
}
