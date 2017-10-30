using Client2.DomainClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Social2.DomainClasses
{
    public class UserFriend
    {
        public User Friend { get; set; }
        public int FriendId { get; set; }

        public User FriendFriend { get; set; }
        public int FriendFriendId { get; set; }


    }
}
