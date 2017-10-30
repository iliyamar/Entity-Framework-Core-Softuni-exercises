
namespace Client2.DomainClasses.CustomAttrValidators
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Text.RegularExpressions;

    public class EmailValidatorAttrtibute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var email = value as string;
            // bool isValid = true;
            if (email == null)
            {
                return false;
            }
            Regex regex = new Regex(@"^[a-zA-Z0-9][a-zA-Z0-9.-_]+@[a-zA-Z0-9]+(\-)?[a-zA-Z0-9]+(\.)?[a-zA-Z0-9]{1,6}?\.[a-zA-Z]{2,6}$");

            if (regex.IsMatch(email))
            {
                return true;
            }


            else return false;
        }

        // E-mail – Required field.Text is in format<user>@<host> where:
        //a.  <user> is a sequence of letters and digits, where '.', '-' and '_' can appear between them (they cannot appear at the beginning or at the end of the sequence). 
        //b.  <host> is a sequence of at least two words, separated by dots '.' (dots cannot appear at the beginning or at the end of the sequence)
    }
}
