using Connectify.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connectify.Models.ViewModels
{
    public class FriendRequestVM
    {
        public FriendRequestVM()
        {

        }
        public FriendRequestVM(FriendsDto friend)
        {
            User1 = friend.User1;
            User2 = friend.User2;
            Active = friend.Active;
        }
        public int User1 { get; set; }
        public int User2 { get; set; }
        public bool Active { get; set; }
    }
}