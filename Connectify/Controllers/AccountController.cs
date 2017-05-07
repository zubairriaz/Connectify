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
            if (user.Equals(UserName))
            {
                userType = "owner";
            }
            ViewBag.userType = userType;

                 
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