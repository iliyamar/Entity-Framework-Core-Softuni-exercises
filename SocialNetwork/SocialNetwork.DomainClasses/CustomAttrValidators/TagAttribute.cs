namespace Social2.DomainClasses.CustomAttrValidators
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public class TagAttribute :ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var tag = value as string;
            bool isvalid = true;

            if (!tag.StartsWith('#'))
            {
                isvalid = false;
            }

            if (tag.Length>20)
            {
                isvalid = false;
            }

            if (tag.Any(a=>Char.IsWhiteSpace(a)))
            {
                isvalid = false;
            }



            return isvalid;
        }

      

    }
}
