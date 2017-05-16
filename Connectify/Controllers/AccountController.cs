using Connectify.Models.Data;
using Connectify.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Connectify.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        public ActionResult Index()
        {
            //confirmed user if they are not loggedin
            string username = User.Identity.Name;
            if (!string.IsNullOrEmpty(username))
            {
                return Redirect("~/" + username);
            }
            return View();
        }
        [HttpPost]
        public ActionResult CreateAccount(UsersVM user, HttpPostedFileBase file)
        {
            Db db = new Db();
            if(!ModelState.IsValid){
                return View("Index", user);
            }
            if (db.Users.Any(x => x.UserName.Equals(user.UserName)))
            {
                ModelState.AddModelError("", "User Name " + user.UserName + " is taken");
                user.UserName = "";
                return View("Index", user);
            }
            UsersDto userDto = new UsersDto
            {
                FirstName=user.FirstName,
                LastName=user.LastName,
                Password=user.Password,
                 UserName=user.UserName

            };
            db.Users.Add(userDto);
            db.SaveChanges();
            int userId = db.Users.Where(x => x.UserName.Equals(user.UserName)).Select(x => x.Id).FirstOrDefault();
            FormsAuthentication.SetAuthCookie(user.UserName, false);
          
            if (file != null && file.ContentLength > 0)
            {
                var fileName =  userId + ".jpg";
                var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
               
                file.SaveAs(path);
               

            }
            Wall wall = new Wall
            {
                DateEdited = System.DateTime.Now,
                Message = "",
                Id = userId
            };
            db.Wall.Add(wall);
            db.SaveChanges();
            return Redirect("~/"+user.UserName);
        }
        //Get
        [Authorize]
        public ActionResult Username(string UserName="")
        {
            Db db = new Db();
            if (!db.Users.Any(x => x.UserName.Equals(UserName)))
            {
                return Redirect("~/");
            }
            //User who is logged in
            string userName = User.Identity.Name;
            UsersDto user = db.Users.Where(x => x.UserName.Equals(userName)).FirstOrDefault();
            ViewBag.FullName = user.FirstName + " " + user.LastName;
            ViewBag.Image = user.Id + ".jpg";
            //User who is viewing
            UsersDto userView = db.Users.Where(x => x.UserName.Equals(UserName)).FirstOrDefault();
            ViewBag.USERNAME = userView.UserName;
            ViewBag.ViewFullName = user.FirstName + " " + user.LastName;
            ViewBag.Image2 = userView.Id + ".jpg";

            string userType = "guest";
            if (UserName.Equals(User.Identity.Name))
            {
                userType = "owner";
            }
            ViewBag.userType = userType;
            if (userType == "guest")
            {
                UsersDto u1 = db.Users.Where(x => x.UserName.Equals(userName)).FirstOrDefault();
                int id1 = u1.Id;
                UsersDto u2 = db.Users.Where(x => x.UserName.Equals(UserName)).FirstOrDefault();
                int id2 = u2.Id;
                FriendsDto f1 = db.Friends.Where(x => x.User1.Equals(id1) && x.User2.Equals(id2)).FirstOrDefault();
                FriendsDto f2 = db.Friends.Where(x => x.User1.Equals(id2) && x.User2.Equals(id1)).FirstOrDefault();
                if (f1 == null && f2 == null)
                {
                    ViewBag.Friends = "False";
                }
                if (f1 != null)
                {
                    if (!f1.Active)
                    {
                        ViewBag.Friends = "Pending";
                    }
                }
                if (f2 != null)
                {
                    if (!f2.Active)
                    {
                        ViewBag.Friends = "Pending";
                    }
                }
               
            }
            var friendCount = db.Friends.Count(x => x.User2 == user.Id && x.Active == false);
            ViewBag.count = user.Id;
            if (friendCount > 0)
            {
                ViewBag.friendsCount = Convert.ToInt32(friendCount.ToString());
            }
            //View Bag Friend Count
            UsersDto userf = db.Users.Where(x => x.UserName.Equals(UserName)).FirstOrDefault();
            int userfId = userf.Id;
            var FriendCount = db.Friends.Count(x => x.User1 == userfId && x.Active == true || x.User2 == userfId && x.Active == true);

            ViewBag.FriendCount = FriendCount;
            UsersDto userf1 = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            int userf1Id = userf1.Id;
            var MessageCount = db.Messages.Count(x => x.To == userf1Id && x.Read == false);
            ViewBag.MessageCount = MessageCount;
            Wall wall = new Wall();
            ViewBag.MessageWall = db.Wall.Where(x => x.Id == userf1Id).Select(x => x.Message).FirstOrDefault();
            ViewBag.UserId = userfId;
            List<int> friendIds1 = db.Friends.Where(x => x.User1 == userf1Id && x.Active == true).Select(x => x.User2).ToList();
            List<int> friendIds2 = db.Friends.Where(x => x.User2 == userf1Id && x.Active == true).Select(x => x.User1).ToList();
            List<int> friendsAll = friendIds1.Concat(friendIds2).ToList();
            List<WallVM> WallMessages = db.Wall.Where(x => friendsAll.Contains(x.Id)).ToArray().Select(x => new WallVM(x)).ToList();
            ViewBag.WallMessages = WallMessages;



           
                 
            return View("View1");
        }
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/");
        }
        public ActionResult LogIn()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult LogIn(LoginVM user)
        {
            Db db = new Db();
            if (db.Users.Any(x => x.UserName.Equals(user.Username) && x.Password.Equals(user.Password)))
            {
                FormsAuthentication.SetAuthCookie(user.Username, false);
                TempData["error"] = "false";
                return Redirect("~/" + user.Username);
               
            }
            else
            {
                TempData["error"] = "true";
                return Redirect("~/");
            }

        }
       
       
}

}
