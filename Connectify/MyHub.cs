using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Diagnostics;
using Connectify.Models.Data;


namespace Connectify
{
    [HubName("echo")]
    public class MyHub : Hub
    {
        public void Hello(string message)
        {
            Trace.WriteLine(message);
            Clients.All.test("this is test");
        }
        public void notify(string friend)
        {
            Db db = new Db();
            UsersDto user = db.Users.Where(x => x.UserName.Equals(friend)).FirstOrDefault();
            int friendId = user.Id;
            var friendCount = db.Friends.Count(x => x.User2 == friendId && x.Active==false);

            var clients = Clients.Others;
            clients.frnotify(friend, friendCount);

        }
        public void getFrCount(string message)
        {
            Db db = new Db();
            UsersDto user = db.Users.Where(x => x.UserName.Equals(Context.User.Identity.Name)).FirstOrDefault();
            int userId = user.Id;
            var friendCount = db.Friends.Count(x => x.User2 == userId && x.Active == false);
            Trace.WriteLine("" + "" + friendCount);
            var clients = Clients.All;
            clients.frcount(friendCount);
           

        }
        public void getFCount(int friendId)
        {  
             Db db = new Db();
             UsersDto user = db.Users.Where(x => x.UserName.Equals(Context.User.Identity.Name)).FirstOrDefault();
             int userId = user.Id;
              var FriendCount = db.Friends.Count(x => x.User1 == userId && x.Active == true || x.User2 == userId && x.Active == true);
              UsersDto user2 = db.Users.Where(x => x.Id == friendId).FirstOrDefault();
              string username = user2.UserName;
              var FriendCount2 = db.Friends.Count(x => x.User1 == friendId && x.Active == true || x.User2 == friendId && x.Active == true);

              Clients.All.fscount(Context.User.Identity.Name, username, FriendCount, FriendCount2);

             

        }
        public void notifyOfMessages(string friend)
        {
            Db db = new Db();
            UsersDto user = db.Users.Where(x => x.UserName.Equals(friend)).FirstOrDefault();
            int friendId = user.Id;
            int mscount = db.Messages.Count(x => x.To == friendId && x.Read == false);
            var clients = Clients.Others;
            clients.msgcount(friend, mscount);

        }
        public void msgnotify()
        {
            Db db = new Db();
            UsersDto user = db.Users.Where(x => x.UserName.Equals(Context.User.Identity.Name)).FirstOrDefault();
            int friendId = user.Id;
            int mscount = db.Messages.Count(x => x.To == friendId && x.Read == false);
            var clients = Clients.Caller;
            clients.msgcount( mscount);

        }

    }
}