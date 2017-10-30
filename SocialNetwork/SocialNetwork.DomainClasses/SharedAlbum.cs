
namespace Social2.DomainClasses
{

    using Client2.DomainClasses;
    using System.Collections.Generic;

    public class SharedAlbum
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int AlbumId { get; set; }
        public Album Album { get; set; }

        public int SharedToId { get; set; }
        public User SharedTo { get; set; }

        
        


    }
}
