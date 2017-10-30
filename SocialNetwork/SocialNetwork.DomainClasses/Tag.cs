
namespace Social2.DomainClasses
{
    using Social2.DomainClasses.CustomAttrValidators;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [Tag]
        
        public string Name { get; set; }

        public List<AlbumTag> Albums { get; set; } = new List<AlbumTag>();

    }
}
