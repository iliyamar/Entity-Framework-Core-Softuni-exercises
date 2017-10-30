namespace Social2.DomainClasses
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AlbumPicture
    {
        public int AlbumId { get; set; }
        public Album Album { get; set; }

        public int PictureId { get; set; }
        public Picture Picture { get; set; }


    }
}
