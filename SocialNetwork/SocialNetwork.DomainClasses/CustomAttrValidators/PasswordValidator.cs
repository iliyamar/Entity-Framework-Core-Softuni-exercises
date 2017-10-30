
namespace Client2.DomainClasses.CustomAttrValidators
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class PasswordValidatorAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            object o = 5;
            var ppp = o as string;

            var passWord = value as string;
            bool isValid = true;

            if (value==null)
            {
                return true;
            }
 

            if (!(passWord.Any(c => Char.IsLower(c))
                && passWord.Any(c => Char.IsDigit(c)) 
                && passWord.Any(c => Char.IsUpper(c))
                && passWord.Any(c => "!@#$%^&*()_+<>?".Contains(c))))
            {
                isValid =false;
            }


            return isValid;



        //a.    1 lowercase letter
        //b.    1 uppercase letter
        //c.    1 digit
        //d.	1 special symbol (!, @, #, $, %, ^, &, *, (, ), _, +, <, >, ?)


           // return base.IsValid(value);
        }


    }
}
