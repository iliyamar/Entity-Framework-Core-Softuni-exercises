namespace Social2.DomainClasses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class Picture
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Caption { get; set; }
        public string Path { get; set; }

        public List<AlbumPicture> Albums { get; set; } = new List<AlbumPicture>();




        //Each picture has title, caption and path on the file system
    }
}
