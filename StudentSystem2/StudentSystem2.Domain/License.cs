using System.ComponentModel.DataAnnotations;

namespace StudentSystem2.Domain
{
    public class License
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int ResourceId { get; set; }
        public Resource Resource { get; set; }
    }
}
