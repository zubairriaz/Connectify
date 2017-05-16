using Connectify.Models.Data;
using Connectify.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Connectify.Controllers
{
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult LiveSearch(string Username)
        {
            Db db = new Db();
            List<LiveSearchVM> users = db.Users.Where(x => x.UserName.Contains(Username) && x.UserName != User.Identity.Name).ToArray().Select(x => new LiveSearchVM(x)).ToList();
            return Json(users,JsonRequestBehavior.AllowGet);
        }
        public void AddFriend(string friend)
        {
            Db db = new Db();
            UsersDto userDto = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            int userId = userDto.Id;
            UsersDto userDto2 = db.Users.Where(x => x.UserName.Equals(friend)).FirstOrDefault();
            int friendId = userDto2.Id;

            FriendsDto FriendDto = new FriendsDto { 
              Active=false,
               User1=userId,
               User2=friendId,
               
            };
            db.Friends.Add(FriendDto);
            db.SaveChanges();
        }
         [HttpPost]
        public JsonResult DisplayFriendRequests()
        {
            Db db = new Db();
            UsersDto user = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            int userId = user.Id;

            List<FriendRequestVM> list = db.Friends.Where(x => x.User2 == userId && x.Active == false).ToArray().Select(x => new FriendRequestVM(x)).ToList();
            List<UsersDto> users = new List<UsersDto>();
            foreach (var item in list)
            {
                var user1 = db.Users.Where(x => x.Id == item.User1).FirstOrDefault();
                users.Add(user1);

            }
            return Json(users , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
         public void AcceptRequest (int fId)
         {
            
             Db db = new Db();
             UsersDto user = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
             int userId = user.Id;
             Console.WriteLine(userId+""+fId);
             FriendsDto friend = db.Friends.Where(x => x.User1 == fId && x.User2 == userId).FirstOrDefault();
             friend.Active = true;
             db.SaveChanges();
           
        }
          [HttpPost]
        public void DeclineRequest(int fId)
        {
            Db db = new Db();
            UsersDto user = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            int userId = user.Id;
           
            FriendsDto friend = db.Friends.Where(x => x.User1 == fId && x.User2 == userId).FirstOrDefault();
            db.Friends.Remove(friend);
            db.SaveChanges();

        }
        [HttpPost]
          public void SendMessage(string friend , string message)
          {
            Db db = new Db();
            UsersDto user1 = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            int fromId = user1.Id;

            UsersDto user2 = db.Users.Where(x => x.UserName.Equals(friend)).FirstOrDefault();
            int toId = user2.Id;

            MessageDto messagedb = new MessageDto
            {
                From = fromId,
                To = toId,
                Message = message,
                Read = false,
                DateSent = DateTime.Now,

            };
            db.Messages.Add(messagedb);
            db.SaveChanges();

          }
        [HttpPost]
        public ActionResult DisplayMessages()
        {
            Db db = new Db();
            UsersDto user1 = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            int userID = user1.Id;
            List<MessageVM> messages = db.Messages.Where(x => x.To == userID && x.Read == false).ToArray().Select(x => new MessageVM(x)).ToList();
            db.Messages.Where(x => x.To == userID && x.Read == false).ToList().ForEach(x => x.Read = true);
            db.SaveChanges();
            return Json(messages, JsonRequestBehavior.AllowGet);
        }
         [HttpPost]
        public void UpdateWallMessage(int id , string message)
        {
            Db db = new Db();
           Wall wall= db.Wall.Find(id);
           wall.Message = message;
           wall.DateEdited = System.DateTime.Now;
           db.SaveChanges();
             
        }
	}

	}
